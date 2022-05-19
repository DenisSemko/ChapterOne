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
    public class BookCollectionService : IBookCollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public BookCollectionService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Book>> GetBooksByCollectionId(Guid id)
        {
            var result = await _unitOfWork.BookCollectionRepository.GetBooksByCollectionId(id);
            return result;
        }

        public async Task<BookCollection> Add(BookCollectionDto bookCollectionDto)
        {
            try
            {
                var book = await _applicationContext.Book.Where(x => x.Id == bookCollectionDto.Book).FirstOrDefaultAsync();
                var collection = await _applicationContext.Collection.Where(x => x.Id == bookCollectionDto.Collection).FirstOrDefaultAsync();
                var existed = await _applicationContext.BookCollection.Where(x => x.Collection == collection).Where(x => x.Book == book).FirstOrDefaultAsync();

                if (existed != null)
                {
                    return null;
                }
                var bookCollection = new BookCollection
                {
                    Id = Guid.NewGuid(),
                    Book = book,
                    Collection = collection
                };
                await _unitOfWork.BookCollectionRepository.Add(bookCollection);
                return bookCollection;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BookCollection> DeleteById(Guid id)
        {
            var result = await _unitOfWork.BookCollectionRepository.DeleteById(id);
            return result;
        }
    }
}
