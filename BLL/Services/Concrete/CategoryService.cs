using BLL.Services.Abstract;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;

namespace BLL.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public CategoryService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Category>> GetByUserId(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.GetByUserId(id);
            return result;
        }

        public async Task<Category> Add(CategoryDto categoryDto)
        {
            try
            {
                var user = await _applicationContext.Users.Where(x => x.Id == categoryDto.User).FirstOrDefaultAsync();
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Name = categoryDto.Name
                };
                await _unitOfWork.CategoryRepository.Add(category);
                return category;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> DeleteById(Guid id)
        {
            var result = await _unitOfWork.CategoryRepository.DeleteById(id);
            return result;
        }
    }
}
