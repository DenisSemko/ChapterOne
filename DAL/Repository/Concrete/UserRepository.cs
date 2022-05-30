using CIL.Helpers;
using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Repository.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext myDbContext) : base(myDbContext) 
        {
        }

        public new async Task<User> GetById(Guid id)
        {
            var result = await myDbContext.Users.Where(o => o.Id == id).Include(o => o.Subscription).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> UpdateProfileImage(Guid userId, string profileImageUrl)
        {
            var user = await GetById(userId);

            if (user != null)
            {
                user.ProfileImage = profileImageUrl;
                await myDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> GetSubscriptionPaymentDays(Guid userId)
        {
            var currentDate = DateTime.Now;
            var user = await GetById(userId);

            if (user != null)
            {
                if(user.TimeSubscriptionPaid != null)
                {
                    var dateDifference = currentDate.DayOfYear - user.TimeSubscriptionPaid.Value.DayOfYear;

                    if (dateDifference == 0)
                    {
                        return true;
                    } else if(dateDifference > 0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return false;
                }
                
            }

            return false;

        }
    }
}
