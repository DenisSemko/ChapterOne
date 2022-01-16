using CIL.Models;
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
        public Task<User> DeleteById(Guid id);
    }
}
