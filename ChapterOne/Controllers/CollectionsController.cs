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
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            this._collectionService = collectionService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IEnumerable<Collection>> GetByUserId(Guid id)
        {
            try
            {
                var result = await _collectionService.GetByUserId(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Collection>> Add(CollectionDto collectionDto)
        {
            try
            {
                var book = await _collectionService.Add(collectionDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Collection>> DeleteById(Guid id)
        {
            try
            {
                var result = await _collectionService.DeleteById(id);

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
