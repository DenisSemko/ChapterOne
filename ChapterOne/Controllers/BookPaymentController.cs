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
    public class BookPaymentController : ControllerBase
    {
        private readonly IBookPaymentService _bookPaymentService;
        private readonly IUnitOfWork _unitOfWork;

        public BookPaymentController(IBookPaymentService bookPaymentService, IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._bookPaymentService = bookPaymentService;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("{userId:Guid}")]
        public async Task<IEnumerable<BookPayment>> GetByUserId(Guid userId)
        {
            try
            {
                var result = await _bookPaymentService.GetByUserId(userId);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{userId:Guid}/send-book")]
        public async Task<bool> SendBook(Guid userId)
        {
            try
            {
                var result = await _bookPaymentService.SendBook(userId);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookPayment>> Add(BookPaymentDto bookDto)
        {
            try
            {
                var book = await _bookPaymentService.Add(bookDto);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IEnumerable<BookPayment>> DeleteByUserId(Guid id)
        {
            try
            {
                var result = await _bookPaymentService.DeleteByUserId(id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
