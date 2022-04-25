using BLL.Services.Abstract;
using CIL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationController : ControllerBase
    {
        private readonly ICombinationService _combinationService;

        public CombinationController(ICombinationService combinationService)
        {
            this._combinationService = combinationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combination>>> GenerateCombination()
        {
            return Ok(await _combinationService.GenerateCombination());
        }

        [HttpPost]
        public async Task<ActionResult<Combination>> Add(Combination combination)
        {
            try
            {
                if (combination == null)
                {
                    return BadRequest();
                }

                var result = await _combinationService.Add(combination);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Combination>> DeleteById(Guid id)
        {
            try
            {
                var result = await _combinationService.DeleteById(id);

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
