using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IBookCollectionService
    {
        public Task<IEnumerable<Book>> GetBooksByCollectionId(Guid id);
        public Task<BookCollection> Add(BookCollectionDto bookCollectionDto);
        public Task<BookCollection> DeleteById(Guid id);
    }
}
