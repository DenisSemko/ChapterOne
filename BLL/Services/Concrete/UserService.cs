using BLL.Services.Abstract;
using CIL.Models;
using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _unitOfWork.UserRepository.Get();
        }

        public async Task<User> GetById(Guid id)
        {
            var result = await _unitOfWork.UserRepository.GetById(id);
            return result;
        }

        public async Task<User> Add(User user)
        {
            var result = await _unitOfWork.UserRepository.Add(user);
            return result;
        }

        public async Task<User> Update(User user)
        {
            var result = await _unitOfWork.UserRepository.Update(user);
            return result;
        }

        public async Task<User> DeleteById(Guid id)
        {
            var result = await _unitOfWork.UserRepository.DeleteById(id);
            return result;
        }
    }
}
