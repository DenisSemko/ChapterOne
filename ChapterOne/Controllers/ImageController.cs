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

        public ImageController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        public IActionResult Get(string fileName)
        {
            var image = System.IO.File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName));
            return File(image, "image/jpeg");
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
