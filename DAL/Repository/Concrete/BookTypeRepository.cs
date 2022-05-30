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
    public class BookTypeRepository : BaseRepository<BooksTypes>, IBookTypeRepository
    {
        public BookTypeRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<IEnumerable<BooksTypes>> GetByBookId(Guid id)
        {
            var bookTypes = await myDbContext.BooksTypes.Where(x => x.Book.Id == id)
                .Include(x => x.Book).Include(x => x.Type).ToListAsync();
            return bookTypes;
        }

        public async Task<BooksTypes> GetWebByBookId(Guid id)
        {
            var bookTypes = await myDbContext.BooksTypes.Where(x => x.Book.Id == id).Where(x => x.Type.Name == "WebFile")
                .Include(x => x.Book).Include(x => x.Type).FirstOrDefaultAsync();
            return bookTypes;
        }

        public async Task<IEnumerable<CIL.Models.Type>> GetTypes()
        {
            var types = await myDbContext.Type.ToListAsync();
            return types;
        }
    }
}
