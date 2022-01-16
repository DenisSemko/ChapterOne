using BLL.Services.Abstract;
using CIL.Models;
using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Subscription>> Get()
        {
            return await _unitOfWork.SubscriptionRepository.Get();
        }

        public async Task<Subscription> Add(Subscription section)
        {
            var result = await _unitOfWork.SubscriptionRepository.Add(section);
            return result;
        }
    }
}
