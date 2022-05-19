using BLL.Services.Abstract;
using CIL.DTOs;
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
    public class BookTypeService : IBookTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public BookTypeService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<BooksTypes>> GetByBookId(Guid id)
        {
            var result = await _unitOfWork.BookTypeRepository.GetByBookId(id);
            return result;
        }

        public async Task<BooksTypes> GetWebByBookId(Guid id)
        {
            var result = await _unitOfWork.BookTypeRepository.GetWebByBookId(id);
            return result;
        }

        public async Task<BooksTypes> Add(BooksTypesDto bookDto)
        {
            try
            {
                var book = await _applicationContext.Book.Where(x => x.Id == bookDto.Book).FirstOrDefaultAsync();
                var type = await _applicationContext.Type.Where(x => x.Id == bookDto.Type).FirstOrDefaultAsync();
                var bookType = new BooksTypes
                {
                    Id = Guid.NewGuid(),
                    Book = book,
                    Type = type,
                    Price = bookDto.Price
                    
                };
                await _unitOfWork.BookTypeRepository.Add(bookType);
                return bookType;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
