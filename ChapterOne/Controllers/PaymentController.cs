using BLL.Services.Abstract;
using CIL.DTOs;
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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        public PaymentController(IPaymentService paymentService, IUserService userService)
        {
            this._paymentService = paymentService;
            this._userService = userService;
        }

        [HttpGet("{userId:Guid}")]
        public async Task BuySubscription(Guid userId)
        {
            var user = await _userService.GetById(userId);
            var paymentData = _paymentService.CreatePayment(user);
        }
    }
}
