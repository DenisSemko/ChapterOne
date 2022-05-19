using CIL.DTOs;
using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetByUserId(Guid id);
        public Task<Category> Add(CategoryDto categoryDto);
        public Task<Category> DeleteById(Guid id);
    }
}
