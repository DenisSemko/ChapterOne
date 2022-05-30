using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CIL.Models;

namespace DAL.Repository.Abstract
{
    public interface ISubscriptionBookRepository : IRepository<SubscriptionsBooks>
    {
        public Task<IEnumerable<Book>> GetBooksBySubscriptionId(Guid id);
        public Task<bool> FindBookInFreeBooks(Guid userId, Guid bookId);
    }
}
