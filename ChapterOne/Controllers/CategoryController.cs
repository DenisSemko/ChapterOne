using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IEnumerable<Category>> GetByUserId(Guid id)
        {
            try
            {
                var result = await _categoryService.GetByUserId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Add(CategoryDto categoryDto)
        {
            try
            {
                var book = await _categoryService.Add(categoryDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Category>> DeleteById(Guid id)
        {
            try
            {
                var result = await _categoryService.DeleteById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
