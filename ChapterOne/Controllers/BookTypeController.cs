using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTypeController : ControllerBase
    {
        private readonly IBookTypeService _bookTypeService;

        public BookTypeController(IBookTypeService bookTypeService)
        {
            this._bookTypeService = bookTypeService;
        }

        [HttpGet]
        public async Task<IEnumerable<CIL.Models.Type>> GetTypes()
        {
            try
            {
                var result = await _bookTypeService.GetTypes();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IEnumerable<BooksTypes>> GetBookById(Guid id)
        {
            try
            {
                var result = await _bookTypeService.GetByBookId(id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:Guid}/web-file")]
        public async Task<BooksTypes> GetWebBookById(Guid id)
        {
            try
            {
                var result = await _bookTypeService.GetWebByBookId(id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<BooksTypes>> Add(BooksTypesDto bookType)
        {
            try
            {
                if (bookType == null)
                {
                    return BadRequest();
                }

                var result = await _bookTypeService.Add(bookType);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
