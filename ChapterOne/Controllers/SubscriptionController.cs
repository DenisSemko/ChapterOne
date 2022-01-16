using BLL.Services.Abstract;
using CIL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this._subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> Get()
        {
            return Ok(await _subscriptionService.Get());
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> Add(Subscription section)
        {
            try
            {
                if (section == null)
                {
                    return BadRequest();
                }

                var result = await _subscriptionService.Add(section);
                return result;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating the new section record");
            }
        }
    }
}
