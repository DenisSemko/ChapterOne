using AutoMapper;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL.Repository.Abstract;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MimeKit;
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
        private readonly EmailConfiguration _emailConfig;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, EmailConfiguration emailConfig)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._mapper = mapper;
            this._emailConfig = emailConfig;
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
               ".jpg",
               ".pdf"
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

        public async Task<bool> SendSubscriptionPayment(Guid userId)
        {
            var user = GetById(userId);
            var subscriptionPayment = GetSubscriptionPaymentDays(user.Result.Id);
            if (subscriptionPayment.Result)
            {
                SendEmail(await user, "pay-subscription.html");

                return true;
            }

            return false;
        }

        public async Task<bool> SendSubscriptionPaymentSuccess(Guid userId)
        {
            var user = GetById(userId);
            if (user != null)
            {
                SendEmail(await user, "pay-subscription-success.html");
                return true;
            }

            return false;
        }

        public async Task<bool> GetSubscriptionPaymentDays(Guid userId)
        {
            var result = await _unitOfWork.UserRepository.GetSubscriptionPaymentDays(userId);
            return result;
        }

        private async void SendEmail(User user, string name)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("ChapterOne", _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(user.UserName, user.Email));
            emailMessage.Subject = "ChapterOne - Subscription Payment";

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources", name);
            string EmailTemplateText = File.ReadAllText(FilePath);
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = EmailTemplateText;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
