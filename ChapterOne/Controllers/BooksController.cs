using AutoMapper;
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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IBookService bookService, IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._bookService = bookService;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return Ok(await _bookService.Get());
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Book>> FindByTitle(string title)
        {
            return Ok(await _bookService.FindByTitle(title));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Book>> GetById(Guid id)
        {
            try
            {
                var result = await _bookService.GetById(id);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Add(BookAddDto bookDto)
        {
            try
            {
                var book = await _bookService.Add(bookDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Book>> DeleteById(Guid id)
        {
            try
            {
                var result = await _bookService.DeleteById(id);

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
