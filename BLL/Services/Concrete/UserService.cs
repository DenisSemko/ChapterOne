using AutoMapper;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._mapper = mapper;
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

        public async Task<User> Update(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            _mapper.Map(userDto, user);
            await _unitOfWork.UserRepository.Update(user);
            return user;
        }

        public async Task<User> DeleteById(Guid id)
        {
            var result = await _unitOfWork.UserRepository.DeleteById(id);
            return result;
        }
    }
}
