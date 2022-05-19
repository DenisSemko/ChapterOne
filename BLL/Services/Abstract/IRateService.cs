using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IRateService
    {
        public Task<IEnumerable<Rate>> Get();
        public Task<Rate> Add(RateDto rateDto);
        public Task<IEnumerable<Rate>> GetByBookId(Guid id);
        public Task<double> GetAverageMarkByBookId(Guid id);
        public Task<double> GetNumberOfReviewsByBookId(Guid id);
    }
}
