using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface ICombinationRepository : IRepository<Combination>
    {
        public Task<Combination> GenerateCombination();
        public Task<Combination> GenerateCombinationFromCollection(Guid userId);
        public string ChangeShortDescriptionBehavior(string shortDescription);
        public Task<IEnumerable<Combination>> LoadOldSchemes(Guid currentUser);
        public Task<IEnumerable<Combination>> GetByUser(Guid user);
    }
}
