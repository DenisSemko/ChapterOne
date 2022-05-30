using BLL.Services.Abstract;
using CIL.Helpers;
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
    public class BooksPaginationController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUnitOfWork _unitOfWork;

        public BooksPaginationController(IBookService bookService, IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._bookService = bookService;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksWithPagination([FromQuery]BookParams bookParams)
        {
            try
            {
                var result = await _bookService.GetBookWithPagination(bookParams);
                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{subscriptionId:Guid}/free-books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksWithPaginationFreeFilter([FromQuery] BookParams bookParams, Guid subscriptionId)
        {
            try
            {
                var result = await _bookService.GetBookWithPaginationFreeFilter(bookParams, subscriptionId);
                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{name}/genre-books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksWithPaginationGenreFilter([FromQuery] BookParams bookParams, string name)
        {
            try
            {
                var result = await _bookService.GetBookWithPaginationGenreFilter(bookParams, name);
                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
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
