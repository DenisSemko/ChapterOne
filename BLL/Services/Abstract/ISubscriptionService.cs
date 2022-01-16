using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface ISubscriptionService
    {
        public Task<IEnumerable<Subscription>> Get();
        public Task<Subscription> Add(Subscription item);
    }
}
