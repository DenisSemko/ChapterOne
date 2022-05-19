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
    public class RateRepository : BaseRepository<Rate>, IRateRepository
    {
        public RateRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public new async Task<IEnumerable<Rate>> Get()
        {
            var result = await myDbContext.Rate
                .Include(o => o.Book).Include(o => o.User)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<Rate>> GetByBookId(Guid id)
        {
            var result = await myDbContext.Rate.Where(o => o.Book.Id == id)
                .Include(o => o.Book).Include(o => o.User)
                .ToListAsync();
            return result;
        }

        public async Task<double> GetAverageMarkByBookId(Guid id)
        {
            var sum = 0.0;
            var ratings = await GetByBookId(id);
            foreach(var rate in ratings)
            {
                sum += rate.Mark;
            }
            var result = sum / ratings.Count();
            return result;
        }

        public async Task<double> GetNumberOfReviewsByBookId(Guid id)
        {
            var ratings = await GetByBookId(id);
            var result = ratings.Count();
            return result;
        }
    }
}
