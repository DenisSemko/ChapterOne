using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IUnitOfWork
    {
        ISubscriptionRepository SubscriptionRepository { get; }
        IUserRepository UserRepository {  get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}
