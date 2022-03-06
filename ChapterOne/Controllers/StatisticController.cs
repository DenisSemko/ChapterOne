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
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        [HttpGet]
        public async Task<SubscriptionUserStatistic> Get()
        {
            var data =  await _statisticService.GetSubscriptionUser();
            var result = new SubscriptionUserStatistic();
            var subscriptionList = new List<string>();
            var numbersList = new List<double>();
            foreach (var d in data)
            {
                subscriptionList.Add(d.Key);
                result.Subscriptions = subscriptionList.ToArray();

                numbersList.Add(d.Value);
                result.Number = numbersList.ToArray();
            }
            return result;
        }
    }
}
