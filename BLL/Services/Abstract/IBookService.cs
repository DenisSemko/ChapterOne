using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> Get();
        public Task<PagedList<Book>> GetBookWithPagination(BookParams bookParams);
        public Task<Book> GetById(Guid id);
        public Task<Book> Add(BookAddDto bookDto);
        //public Task<User> Update(User book);
        public Task<Book> DeleteById(Guid id);
        public Task<Book> FindByTitle(string title);
        public Task<string> UploadImageFiles(Guid bookId, IFormFile bookFile);
    }
}
