using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IBookPaymentService
    {
        public Task<IEnumerable<BookPayment>> GetByUserId(Guid userId);
        public Task<BookPayment> Add(BookPaymentDto bookPaymentDto);
        public Task<IEnumerable<BookPayment>> DeleteByUserId(Guid userId);
        public Task<bool> SendBook(Guid userId);
    }
}
