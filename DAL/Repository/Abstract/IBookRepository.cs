using CIL.Helpers;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        public new Task<IEnumerable<Book>> Get();
        public Task<PagedList<Book>> GetBookWithPagination(BookParams bookParams);
        public Task<PagedList<Book>> GetBookWithPaginationFreeFilter(BookParams bookParams, Guid subscriptionId);
        public Task<PagedList<Book>> GetBookWithPaginationGenreFilter(BookParams bookParams, string name);
        public new Task<Book> GetById(Guid id);
        public Task<Book> FindByISBN(string isbn);
        public Task<Book> FindByTitle(string title);
        public Task<bool> UpdateBookImage(Guid bookId, string bookImageUrl);
        public Task<bool> UpdateBookWebFile(Guid bookId, string bookImageUrl);
        public Task<bool> UpdateBookAudioFile(Guid bookId, string bookImageUrl);
    }
}
