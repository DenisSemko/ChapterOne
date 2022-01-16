﻿using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext myDbContext;

        public UnitOfWork(ApplicationContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public ISubscriptionRepository SubscriptionRepository => new SubscriptionRepository(myDbContext);
        public IUserRepository UserRepository => new UserRepository(myDbContext);

        public void Complete()
        {
            myDbContext.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            myDbContext.ChangeTracker.DetectChanges();
            var changes = myDbContext.ChangeTracker.HasChanges();

            return changes;
        }
    }
}
