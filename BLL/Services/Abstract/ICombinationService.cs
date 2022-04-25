using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface ICombinationService
    {
        public Task<Combination> GenerateCombination();
        public Task<Combination> Add(Combination item);
        public Task<Combination> DeleteById(Guid id);
    }
}
