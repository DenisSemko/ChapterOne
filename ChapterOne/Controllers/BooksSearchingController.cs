using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services.Abstract;
using CIL.Models;

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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Book>>> FindBooks(Combination combination)
        {
            return Ok(await _booksService.FindBooks(combination));
        }
    }
}
