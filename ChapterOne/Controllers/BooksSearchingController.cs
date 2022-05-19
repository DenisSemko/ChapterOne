using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services.Abstract;
using CIL.Models;
using CIL.DTOs;
using CIL.Helpers;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksSearchingController : ControllerBase
    {
        private readonly IBooksSearchingService _booksService;

        public BooksSearchingController(IBooksSearchingService booksService)
        {
            this._booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> FindBooks([FromQuery] CombinationDto combination, [FromQuery] BookParams bookParams)
        {
            var result = await _booksService.FindBooks(combination);
            return Ok(result);
        }
    }
}
