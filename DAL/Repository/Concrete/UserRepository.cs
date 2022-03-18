using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext myDbContext) : base(myDbContext) { }

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
    }
}
