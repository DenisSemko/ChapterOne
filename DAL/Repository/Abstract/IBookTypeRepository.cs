using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IBookTypeRepository : IRepository<BooksTypes>
    {
        public Task<IEnumerable<BooksTypes>> GetByBookId(Guid id);
        public Task<BooksTypes> GetWebByBookId(Guid id);
        public Task<IEnumerable<CIL.Models.Type>> GetTypes();
    }
}
