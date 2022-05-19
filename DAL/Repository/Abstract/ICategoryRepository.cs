using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IEnumerable<Category>> GetByUserId(Guid id);
    }
}
