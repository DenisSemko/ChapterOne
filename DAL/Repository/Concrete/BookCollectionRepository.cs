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
    public class BookCollectionRepository : BaseRepository<BookCollection>, IBookCollectionRepository
    {
        public BookCollectionRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<IEnumerable<Book>> GetBooksByCollectionId(Guid id)
        {
            var bookCollections = await myDbContext.BookCollection.Where(o => o.Collection.Id == id)
                .Include(o => o.Book).Include(o => o.Collection)
                .ToListAsync();
            var books = bookCollections.Select(x => x.Book).Distinct().ToList();
            return books;
        }
    }
}
