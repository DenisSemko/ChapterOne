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
    public class BookPaymentRepository : BaseRepository<BookPayment>, IBookPaymentRepository
    {
        public BookPaymentRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<IEnumerable<BookPayment>> GetByUserId(Guid userid)
        {
            var result = await myDbContext.BookPayments.Where(o => o.User.Id == userid)
                .Include(o => o.Book).Include(o => o.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BookPayment>> DeleteByUserId(Guid userid)
        {
            var result = await myDbContext.BookPayments.Where(o => o.User.Id == userid)
                .Include(o => o.Book).Include(o => o.User)
                .ToListAsync();
            myDbContext.BookPayments.RemoveRange(result);
            await myDbContext.SaveChangesAsync();
            return result;
        }
    }
}
