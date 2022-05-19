using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        public Task<IEnumerable<Collection>> GetByUserId(Guid id);
        public new Task<Collection> DeleteById(Guid id);
    }
}
