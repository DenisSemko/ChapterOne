using BLL.Services.Abstract;
using CIL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookImageFilesController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IFileContentTypeService _fileContentTypeService;

        public BookImageFilesController(IBookService bookService, IFileContentTypeService fileContentTypeService)
        {
            this._bookService = bookService;
            this._fileContentTypeService = fileContentTypeService;
        }
        [HttpGet]
        public IActionResult Get(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Files", fileName);
            var image = System.IO.File.OpenRead(path);
            return File(image, _fileContentTypeService.GetContentType(path));
        }

        [HttpGet("{userId}/{bookId}/send-free-book")]
        public async Task<ActionResult<bool>> SendFreeBook(Guid userId, Guid bookId)
        {
            try
            {
                var result = await _bookService.SendFreeBook(userId, bookId);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("{bookId:Guid}/upload-files")]
        public async Task<ActionResult<string>> UploadImage([FromRoute] Guid bookId, IFormFile profileImage)
        {
            try
            {
                var result = await _bookService.UploadImageFiles(bookId, profileImage);

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
