using AutoMapper;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
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
        private readonly EmailConfiguration _emailConfig;

        public BookService(IUnitOfWork unitOfWork, ApplicationContext applicationContext, EmailConfiguration emailConfig)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
            this._emailConfig = emailConfig;
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _unitOfWork.BookRepository.Get();
        }

        public async Task<PagedList<Book>> GetBookWithPagination(BookParams bookParams)
        {
            return await _unitOfWork.BookRepository.GetBookWithPagination(bookParams);
        }

        public async Task<PagedList<Book>> GetBookWithPaginationFreeFilter(BookParams bookParams, Guid subscriptionId)
        {
            return await _unitOfWork.BookRepository.GetBookWithPaginationFreeFilter(bookParams, subscriptionId);
        }

        public async Task<PagedList<Book>> GetBookWithPaginationGenreFilter(BookParams bookParams, string name)
        {
            return await _unitOfWork.BookRepository.GetBookWithPaginationGenreFilter(bookParams, name);
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
                ".txt",
                ".epub",
                ".mobi",
                ".fb2",
                ".doc",
                ".docx"
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


        public async Task<bool> SendFreeBook(Guid userId, Guid bookId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var book = await GetById(bookId);

            var fileName = book.BookWebFile.Substring(16);
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Files", fileName);
            if(path != null)
            {
                SendFreeBookFile(user, "send-free-book.html", path);
                return true;
            }
            return false;
        }

        private async void SendFreeBookFile(User user, string name, string filename)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("ChapterOne", _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(user.UserName, user.Email));
            emailMessage.Subject = "ChapterOne - Book";

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources", name);
            string EmailTemplateText = File.ReadAllText(FilePath);
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = EmailTemplateText;
            emailBodyBuilder.Attachments.Add(filename);
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            

            var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
