using CIL.Helpers;
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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<IEnumerable<Category>> GetByUserId(Guid id)
        {
            var result = await myDbContext.Category.Where(o => o.User.Id == id)
                .Include(o => o.User)
                .ToListAsync();
            return result;
        }
    }
}
