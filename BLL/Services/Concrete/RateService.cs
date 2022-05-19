using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class RateService : IRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public RateService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Rate>> Get()
        {
            return await _unitOfWork.RateRepository.Get();
        }

        public async Task<Rate> Add(RateDto rateDto)
        {
            try
            {
                var book = await _applicationContext.Book.Where(x => x.Id == rateDto.Book).FirstOrDefaultAsync();
                var user = await _applicationContext.Users.Where(x => x.Id == rateDto.User).FirstOrDefaultAsync();
                var rate = new Rate
                {
                    Id = Guid.NewGuid(),
                    Mark = rateDto.Mark,
                    Comment = rateDto.Comment,
                    Date = rateDto.Date,
                    Book = book,
                    User = user
                };
                await _unitOfWork.RateRepository.Add(rate);
                return rate;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Rate>> GetByBookId(Guid id)
        {
            var result = await _unitOfWork.RateRepository.GetByBookId(id);
            return result;
        }

        public async Task<double> GetAverageMarkByBookId(Guid id)
        {
            var result = await _unitOfWork.RateRepository.GetAverageMarkByBookId(id);
            return result;
        }

        public async Task<double> GetNumberOfReviewsByBookId(Guid id)
        {
            var result = await _unitOfWork.RateRepository.GetNumberOfReviewsByBookId(id);
            return result;
        }
    }
}
