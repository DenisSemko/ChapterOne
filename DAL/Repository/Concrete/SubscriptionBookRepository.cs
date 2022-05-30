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
    public class SubscriptionBookRepository : BaseRepository<SubscriptionsBooks>, ISubscriptionBookRepository
    {
        public SubscriptionBookRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<IEnumerable<Book>> GetBooksBySubscriptionId(Guid id)
        {
            var bookSubscriptions = await myDbContext.SubscriptionsBooks.Where(o => o.Subscription.Id == id)
                .Include(o => o.Book).Include(o => o.Subscription)
                .ToListAsync();
            var books = bookSubscriptions.Select(x => x.Book).Distinct().ToList();
            return books;
        }
        public async Task<bool> FindBookInFreeBooks(Guid userId, Guid bookId)
        {
            var user = await myDbContext.Users.Where(x => x.Id == userId).Include(x => x.Subscription).FirstOrDefaultAsync();
            var subscription = user.Subscription;
            var bookSubscriptions = await myDbContext.SubscriptionsBooks.Where(o => o.Subscription.Id == subscription.Id)
                .Where(o => o.Book.Id == bookId)
                .Include(o => o.Book).Include(o => o.Subscription)
                .FirstOrDefaultAsync();
            if(bookSubscriptions != null)
            {
                return true;
            }
            return false;
        }
    }
}
