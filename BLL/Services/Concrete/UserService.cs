using AutoMapper;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<string> UploadImage(Guid userId, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };

            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if (await _unitOfWork.UserRepository.GetById(userId) != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await _unitOfWork.ImageRepository.Upload(profileImage, fileName);

                        if (await _unitOfWork.UserRepository.UpdateProfileImage(userId, fileImagePath))
                        {
                            return fileImagePath;
                        }

                        return "Error uploading image";
                    }
                }

                return "This is not a valid Image format";
            }

            return "Not Found";
        }
    }
}
