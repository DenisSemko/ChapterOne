using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
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
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;
        public RateController(IRateService rateService)
        {
            this._rateService = rateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> Get()
        {
            return Ok(await _rateService.Get());
        }

        [HttpGet("{id:Guid}")]
        public async Task<IEnumerable<Rate>> GetByBookId(Guid id)
        {
            try
            {
                var result = await _rateService.GetByBookId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:Guid}/average-mark")]
        public async Task<double> GetAverageMarkByBookId(Guid id)
        {
            try
            {
                var result = await _rateService.GetAverageMarkByBookId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:Guid}/number-reviews")]
        public async Task<double> GetNumberOfReviewsByBookId(Guid id)
        {
            try
            {
                var result = await _rateService.GetNumberOfReviewsByBookId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Rate>> Add(RateDto rateDto)
        {
            try
            {
                var book = await _rateService.Add(rateDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
