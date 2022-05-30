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
        IImageRepository ImageRepository {  get; }
        IBookRepository BookRepository {  get; }
        IGenreRepository GenreRepository {  get; }
        ICombinationRepository CombinationRepository {  get; }
        IBookTypeRepository BookTypeRepository {  get; }
        IRateRepository RateRepository {  get; }
        IBookCollectionRepository BookCollectionRepository {  get; }
        ICategoryRepository CategoryRepository {  get; }
        ICollectionRepository CollectionRepository {  get; }
        ISubscriptionBookRepository SubscriptionBookRepository {  get; }
        IBookPaymentRepository BookPaymentRepository {  get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}
