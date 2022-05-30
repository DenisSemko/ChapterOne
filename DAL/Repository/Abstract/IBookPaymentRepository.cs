using CIL.Helpers;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IBookPaymentRepository : IRepository<BookPayment>
    {
        public Task<IEnumerable<BookPayment>> GetByUserId(Guid userId);
        public Task<IEnumerable<BookPayment>> DeleteByUserId(Guid userId);
    }
}
