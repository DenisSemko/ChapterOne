using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IBookCollectionRepository : IRepository<BookCollection>
    {
        public Task<IEnumerable<Book>> GetBooksByCollectionId(Guid id);

    }
}
