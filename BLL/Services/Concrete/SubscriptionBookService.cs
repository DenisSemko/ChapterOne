using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class SubscriptionBookService : ISubscriptionBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public SubscriptionBookService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Book>> GetBooksBySubscriptionId(Guid id)
        {
            var result = await _unitOfWork.SubscriptionBookRepository.GetBooksBySubscriptionId(id);
            return result;
        }

        public async Task<bool> FindBookInFreeBooks(Guid userId, Guid bookId)
        {
            var result = await _unitOfWork.SubscriptionBookRepository.FindBookInFreeBooks(userId, bookId);
            return result;
        }

        public async Task<SubscriptionsBooks> Add(SubscriptionBookDto subscriptionBookDto)
        {
            try
            {
                var book = await _applicationContext.Book.Where(x => x.Id == subscriptionBookDto.Book).FirstOrDefaultAsync();
                var subscription = await _applicationContext.Subscription.Where(x => x.Id == subscriptionBookDto.Subscription).FirstOrDefaultAsync();

                var subscriptionBook = new SubscriptionsBooks
                {
                    Id = Guid.NewGuid(),
                    Book = book,
                    Subscription = subscription
                };
                await _unitOfWork.SubscriptionBookRepository.Add(subscriptionBook);
                return subscriptionBook;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
