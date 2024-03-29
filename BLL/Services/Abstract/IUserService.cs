﻿using CIL.DTOs;
using CIL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> Get();
        public Task<User> GetById(Guid id);
        public Task<User> Add(User item);
        public Task<User> Update(User item);
        public Task<User> UpdateUserSubscription(string username);
        public Task<User> DeleteById(Guid id);
        public Task<string> UploadImage(Guid userId, IFormFile profileImage);
        public Task<bool> SendSubscriptionPayment(Guid user);
        public Task<bool> SendSubscriptionPaymentSuccess(Guid userId);
        public Task<bool> GetSubscriptionPaymentDays(Guid userId);
    }
}
