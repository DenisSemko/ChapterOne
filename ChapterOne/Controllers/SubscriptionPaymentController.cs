using BLL.Services.Abstract;
using CIL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPaymentController : ControllerBase
    {
        private readonly IUserService _userService;

        public SubscriptionPaymentController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<bool>> GetSubscriptionPaymentDays(Guid id)
        {
            try
            {
                var result = await _userService.GetSubscriptionPaymentDays(id);

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{user:Guid}/send-email")]
        public async Task<ActionResult<bool>> SendSubscriptionPayment(Guid user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var result = await _userService.SendSubscriptionPayment(user);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
