using BLL.Services.Abstract;
using CIL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IUserService _userService;

        public UserAuthController(IUserAuthService userAuthService, IUserService userService)
        {
            this._userAuthService = userAuthService;
            this._userService = userService;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var authResponse = await _userAuthService.RegisterAsync(registerModel);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                AccessToken = authResponse.AccessToken,
                Username = authResponse.Username
            });
        }

        [HttpPost]
        [Route("registration-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
        {
            var authResponse = await _userAuthService.RegisterAdminAsync(registerModel);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                AccessToken = authResponse.AccessToken,
                Username = authResponse.Username
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var authResponse = await _userAuthService.LoginAsync(loginModel);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                AccessToken = authResponse.AccessToken,
                Username = authResponse.Username
            });
        }
    }
}
