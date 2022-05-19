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
    public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<IEnumerable<Collection>> GetByUserId(Guid id)
        {
            var result = await myDbContext.Collection.Where(o => o.User.Id == id)
                .Include(o => o.User).Include(o => o.Category)
                .ToListAsync();
            return result;
        }

        public new async Task<Collection> DeleteById(Guid id)
        {
            var bookCollections = await myDbContext.BookCollection.Where(b => b.Collection.Id == id).ToListAsync();
            myDbContext.BookCollection.RemoveRange(bookCollections);
            var result = await myDbContext.Collection.Where(o => o.Id == id).FirstOrDefaultAsync();
            myDbContext.Collection.Remove(result);
            await myDbContext.SaveChangesAsync();
            return result;
        }
    }
}
