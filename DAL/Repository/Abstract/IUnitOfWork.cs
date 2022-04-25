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

        Task<bool> Complete();
        bool HasChanges();
    }
}
