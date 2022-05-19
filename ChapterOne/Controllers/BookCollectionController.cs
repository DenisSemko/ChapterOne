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
    public class BookCollectionController : ControllerBase
    {
        private readonly IBookCollectionService _bookCollectionService;

        public BookCollectionController(IBookCollectionService bookCollectionService)
        {
            this._bookCollectionService = bookCollectionService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IEnumerable<Book>> GetBooksByCollectionId(Guid id)
        {
            try
            {
                var result = await _bookCollectionService.GetBooksByCollectionId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookCollection>> Add(BookCollectionDto bookCollectionDto)
        {
            try
            {
                var book = await _bookCollectionService.Add(bookCollectionDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<BookCollection>> DeleteById(Guid id)
        {
            try
            {
                var result = await _bookCollectionService.DeleteById(id);

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
