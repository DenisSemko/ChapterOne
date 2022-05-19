using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IRateRepository : IRepository<Rate>
    {
        public new Task<IEnumerable<Rate>> Get();
        public Task<IEnumerable<Rate>> GetByBookId(Guid id);
        public Task<double> GetAverageMarkByBookId(Guid id);
        public Task<double> GetNumberOfReviewsByBookId(Guid id);
    }
}
