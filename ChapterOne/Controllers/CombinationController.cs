using BLL.Services.Abstract;
using CIL.DTOs;
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
        [HttpGet("{userId:Guid}/from-collection")]
        public async Task<ActionResult<IEnumerable<Combination>>> GenerateCombinationFromCollection(Guid userId)
        {
            return Ok(await _combinationService.GenerateCombinationFromCollection(userId));
        }

        [HttpGet("{user:Guid}/by-user")]
        public async Task<ActionResult<IEnumerable<Combination>>> GetByUser(Guid user)
        {
            return Ok(await _combinationService.GetByUser(user));
        }

        [HttpGet("{user:Guid}/load-old")]
        public async Task<ActionResult<IEnumerable<Combination>>> LoadOldSchemes(Guid user)
        {
            return Ok(await _combinationService.LoadOldSchemes(user));
        }

        [HttpPost]
        public async Task<ActionResult<Combination>> Add(CombinationDto combination)
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

        [HttpPut]
        public async Task<ActionResult<Combination>> Update(CombinationDto combinationDto)
        {
            try
            {
                if (combinationDto == null)
                {
                    return BadRequest();
                }

                var result = await _combinationService.Update(combinationDto);
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
