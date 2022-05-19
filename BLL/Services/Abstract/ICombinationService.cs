using CIL.DTOs;
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
        public Task<Combination> GenerateCombinationFromCollection(Guid userId);
        public Task<IEnumerable<Combination>> LoadOldSchemes(Guid currentUser);
        public Task<IEnumerable<Combination>> GetByUser(Guid user);
        public Task<Combination> Add(CombinationDto item);
        public Task<Combination> Update(CombinationDto combinationDto);
        public Task<Combination> DeleteById(Guid id);
    }
}
