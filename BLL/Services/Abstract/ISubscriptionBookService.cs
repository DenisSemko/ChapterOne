using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CIL.DTOs;
using CIL.Models;

namespace BLL.Services.Abstract
{
    public interface ISubscriptionBookService
    {
        public Task<IEnumerable<Book>> GetBooksBySubscriptionId(Guid id);
        public Task<bool> FindBookInFreeBooks(Guid userId, Guid bookId);
        public Task<SubscriptionsBooks> Add(SubscriptionBookDto subscriptionBookDto);
    }
}
