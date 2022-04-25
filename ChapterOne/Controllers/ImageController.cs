using BLL.Services.Abstract;
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
    public class ImageController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFileContentTypeService _fileContentTypeService;

        public ImageController(IUserService userService, IFileContentTypeService fileContentTypeService)
        {
            this._userService = userService;
            this._fileContentTypeService = fileContentTypeService;
        }
        [HttpGet]
        public IActionResult Get(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Files", fileName);
            var image = System.IO.File.OpenRead(path);
            return File(image, _fileContentTypeService.GetContentType(path));
        }

        [HttpPost("{userId:Guid}/upload-image")]
        public async Task<ActionResult<string>> UploadImage([FromRoute] Guid userId, IFormFile profileImage)
        {
            try
            {
                var result = await _userService.UploadImage(userId, profileImage);

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
