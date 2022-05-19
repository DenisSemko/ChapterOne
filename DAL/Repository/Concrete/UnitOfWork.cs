using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
        }

        public ISubscriptionRepository SubscriptionRepository => new SubscriptionRepository(_applicationContext);
        public IUserRepository UserRepository => new UserRepository(_applicationContext);
        public IImageRepository ImageRepository => new ImageRepository();
        public IBookRepository BookRepository => new BookRepository(_applicationContext);
        public IGenreRepository GenreRepository => new GenreRepository(_applicationContext);
        public ICombinationRepository CombinationRepository => new CombinationRepository(_applicationContext);
        public IBookTypeRepository BookTypeRepository => new BookTypeRepository(_applicationContext);
        public IRateRepository RateRepository => new RateRepository(_applicationContext);
        public IBookCollectionRepository BookCollectionRepository => new BookCollectionRepository(_applicationContext);
        public ICategoryRepository CategoryRepository => new CategoryRepository(_applicationContext);
        public ICollectionRepository CollectionRepository => new CollectionRepository(_applicationContext);

        public async Task<bool> Complete()
        {
           return await _applicationContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            _applicationContext.ChangeTracker.DetectChanges();
            var changes = _applicationContext.ChangeTracker.HasChanges();

            return changes;
        }
    }
}
