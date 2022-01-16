using CIL.Models;
using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository.Concrete
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationContext myDbContext) : base(myDbContext) { }
    }
}
