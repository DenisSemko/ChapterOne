using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Abstract
{
    public interface IUnitOfWork
    {
        ISubscriptionRepository SubscriptionRepository { get; }
        IUserRepository UserRepository {  get; }
        
        void Complete();
        bool HasChanges();
    }
}
