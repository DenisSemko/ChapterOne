using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IBookTypeService
    {
        public Task<IEnumerable<BooksTypes>> GetByBookId(Guid id);
        public Task<BooksTypes> GetWebByBookId(Guid id);
        public Task<IEnumerable<CIL.Models.Type>> GetTypes();
        public Task<BooksTypes> Add(BooksTypesDto bookDto);
    }
}
