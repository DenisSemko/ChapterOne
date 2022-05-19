using AutoMapper;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public BookService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _unitOfWork.BookRepository.Get();
        }

        public async Task<PagedList<Book>> GetBookWithPagination(BookParams bookParams)
        {
            return await _unitOfWork.BookRepository.GetBookWithPagination(bookParams);
        }
        public async Task<Book> GetById(Guid id)
        {
            var result = await _unitOfWork.BookRepository.GetById(id);
            return result;
        }

        public async Task<Book> Add(BookAddDto bookDto)
        {
            try
            {
                var genre = await _applicationContext.Genre.Where(x => x.Id == bookDto.Genre).FirstOrDefaultAsync();
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Publisher = bookDto.Publisher,
                    PublishedDate = bookDto.PublishedDate,
                    Genre = genre,
                    Language = bookDto.Language,
                    ISBN = bookDto.ISBN,
                    ShortDescription = bookDto.ShortDescription,
                    ReadingAge = bookDto.ReadingAge,
                    IsQualified = false,
                    File = null,
                    Image = null,
                    UploadDate = DateTime.Now
                };
                await _unitOfWork.BookRepository.Add(book);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Book> DeleteById(Guid id)
        {
            var result = await _unitOfWork.BookRepository.DeleteById(id);
            return result;
        }

        public async Task<Book> FindByTitle(string title)
        {
            var result = await _unitOfWork.BookRepository.FindByTitle(title);
            return result;
        }

        public async Task<string> UploadImageFiles(Guid bookId, IFormFile bookFiles)
        {
            var validImageExtensions = new List<string>
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };

            var validWebExtensions = new List<string>
            {
                ".pdf",
                ".txt"
            };

            var validAudioExtension = new List<string>
            {
                ".mp3",
                ".zip"
            };

            if (bookFiles != null && bookFiles.Length > 0)
            {
                var extension = Path.GetExtension(bookFiles.FileName);
                if (validImageExtensions.Contains(extension))
                {
                    if (await _unitOfWork.BookRepository.GetById(bookId) != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(bookFiles.FileName);

                        var fileImagePath = await _unitOfWork.ImageRepository.Upload(bookFiles, fileName);

                        if (await _unitOfWork.BookRepository.UpdateBookImage(bookId, fileImagePath))
                        {
                            return fileImagePath;
                        }

                        return "Error uploading image";
                    }
                } else if(validWebExtensions.Contains(extension))
                {
                    if (await _unitOfWork.BookRepository.GetById(bookId) != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(bookFiles.FileName);

                        var fileImagePath = await _unitOfWork.ImageRepository.Upload(bookFiles, fileName);

                        if (await _unitOfWork.BookRepository.UpdateBookWebFile(bookId, fileImagePath))
                        {
                            return fileImagePath;
                        }

                        return "Error uploading web file";
                    }

                } else if(validAudioExtension.Contains(extension))
                {
                    if (await _unitOfWork.BookRepository.GetById(bookId) != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(bookFiles.FileName);

                        var fileImagePath = await _unitOfWork.ImageRepository.Upload(bookFiles, fileName);

                        if (await _unitOfWork.BookRepository.UpdateBookAudioFile(bookId, fileImagePath))
                        {
                            return fileImagePath;
                        }

                        return "Error uploading audio file";
                    }
                }

                return "This is not a valid format";
            }

            return "Not Found";
        }
    }
}
