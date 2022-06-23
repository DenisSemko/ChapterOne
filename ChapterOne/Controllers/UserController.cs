using AutoMapper;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, ApplicationContext context, IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<User> userManager)
        {
            this._userService = userService;
            this._context = context;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await _userService.Get());
        }

        [HttpGet("current-user")]
        public ActionResult<Guid> GetCurrentUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("id")?.Value;
            return Guid.Parse(userId);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            try
            {
                var result = await _context.Users.Where(o => o.Id == id).Include(o => o.Subscription).FirstOrDefaultAsync();
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
        public async Task<ActionResult<User>> Add(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var result = await _userService.Add(user);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update(UserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDto.Username);
                _mapper.Map(userDto, user);
                await _unitOfWork.UserRepository.Update(user);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        [HttpGet("{username}/update-subscription")]
        public async Task<ActionResult<User>> UpdateUserSubscription(string username)
        {
            try
            {
                var user = await _userService.UpdateUserSubscription(username);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<User>> DeleteById(Guid id)
        {
            try
            {
                var result = await _userService.DeleteById(id);

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
