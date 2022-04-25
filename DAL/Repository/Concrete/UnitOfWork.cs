using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public IImageRepository ImageRepository => new ImageRepository();
        public IBookRepository BookRepository => new BookRepository(myDbContext);
        public IGenreRepository GenreRepository => new GenreRepository(myDbContext);
        public ICombinationRepository CombinationRepository => new CombinationRepository(myDbContext);

        public async Task<bool> Complete()
        {
           return await myDbContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            myDbContext.ChangeTracker.DetectChanges();
            var changes = myDbContext.ChangeTracker.HasChanges();

            return changes;
        }
    }
}
