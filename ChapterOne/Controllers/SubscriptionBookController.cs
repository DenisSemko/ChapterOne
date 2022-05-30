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
    public class SubscriptionBookController : ControllerBase
    {
        private readonly ISubscriptionBookService _subscriptionBookService;

        public SubscriptionBookController(ISubscriptionBookService subscriptionBookService)
        {
            this._subscriptionBookService = subscriptionBookService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IEnumerable<Book>> GetBooksBySubscriptionId(Guid id)
        {
            try
            {
                var result = await _subscriptionBookService.GetBooksBySubscriptionId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{userId:Guid}/{bookId:Guid}")]
        public async Task<bool> FindBookInFreeBooks(Guid userId, Guid bookId)
        {
            try
            {
                var result = await _subscriptionBookService.FindBookInFreeBooks(userId, bookId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionsBooks>> Add(SubscriptionBookDto subscriptionBookDto)
        {
            try
            {
                var book = await _subscriptionBookService.Add(subscriptionBookDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
