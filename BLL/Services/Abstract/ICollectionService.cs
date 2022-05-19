using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface ICollectionService
    {
        public Task<IEnumerable<Collection>> GetByUserId(Guid id);
        public Task<Collection> Add(CollectionDto collectionDto);
        public Task<Collection> DeleteById(Guid id);
    }
}
