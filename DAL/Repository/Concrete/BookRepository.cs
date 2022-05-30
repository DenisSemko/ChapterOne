using CIL.Helpers;
using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concrete
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public new async Task<IEnumerable<Book>> Get()
        {
            var result = await myDbContext.Book
                .Include(o => o.Genre).Include(o => o.Image).Include(o => o.File)
                .ToListAsync();
            return result;
        }

        public async Task<PagedList<Book>> GetBookWithPagination(BookParams bookParams)
        {
            var result = myDbContext.Book
               .Include(o => o.Genre).Include(o => o.Image).Include(o => o.File).AsNoTracking();
            return await PagedList<Book>.CreateAsync(result, bookParams.PageNumber, bookParams.PageSize);
        }

        public async Task<PagedList<Book>> GetBookWithPaginationFreeFilter(BookParams bookParams, Guid subscriptionId)
        {
            var bookSubscriptions = myDbContext.SubscriptionsBooks.Where(o => o.Subscription.Id == subscriptionId)
                .Include(o => o.Book).Include(o => o.Subscription)
                .AsNoTracking();
            var books = bookSubscriptions.Select(x => x.Book).AsNoTracking();
            return await PagedList<Book>.CreateAsync(books, bookParams.PageNumber, bookParams.PageSize);
        }

        public async Task<PagedList<Book>> GetBookWithPaginationGenreFilter(BookParams bookParams, string name)
        {
            var books = myDbContext.Book.Where(o => o.Genre.Name == name)
                .Include(o => o.Genre).Include(o => o.Image).Include(o => o.File)
                .AsNoTracking();
            return await PagedList<Book>.CreateAsync(books, bookParams.PageNumber, bookParams.PageSize);
        }

        public new async Task<Book> GetById(Guid id)
        {
            var result = await myDbContext.Book.Where(o => o.Id == id)
                .Include(o => o.Genre).Include(o => o.Image).Include(o => o.File)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Book> FindByISBN(string isbn)
        {
            var result = await myDbContext.Book.Where(o => o.ISBN == isbn)
                .Include(o => o.Genre).Include(o => o.Image).Include(o => o.File)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<Book> FindByTitle(string title)
        {
            var result = await myDbContext.Book.Where(o => o.Title == title)
                .Include(o => o.Genre).Include(o => o.Image).Include(o => o.File)
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> UpdateBookImage(Guid bookId, string bookImageUrl)
        {
            var book = await GetById(bookId);

            if (book != null)
            {
                book.BookImage = bookImageUrl;
                await myDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> UpdateBookWebFile(Guid bookId, string bookImageUrl)
        {
            var book = await GetById(bookId);

            if (book != null)
            {
                book.BookWebFile = bookImageUrl;
                await myDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> UpdateBookAudioFile(Guid bookId, string bookImageUrl)
        {
            var book = await GetById(bookId);

            if (book != null)
            {
                book.BookAudioFile = bookImageUrl;
                await myDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
