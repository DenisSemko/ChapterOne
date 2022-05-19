using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class CombinationService : ICombinationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public CombinationService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<Combination> GenerateCombination()
        {
            var result = await _unitOfWork.CombinationRepository.GenerateCombination();
            return result;
        }

        public async Task<Combination> GenerateCombinationFromCollection(Guid userId)
        {
            var result = await _unitOfWork.CombinationRepository.GenerateCombinationFromCollection(userId);
            return result;
        }

        public async Task<IEnumerable<Combination>> LoadOldSchemes(Guid currentUser)
        {
            var result = await _unitOfWork.CombinationRepository.LoadOldSchemes(currentUser);
            return result;
        }
        public async Task<IEnumerable<Combination>> GetByUser(Guid user)
        {
            var result = await _unitOfWork.CombinationRepository.GetByUser(user);
            return result;
        }

        public async Task<Combination> Add(CombinationDto combinationDto)
        {
            var reader = await _applicationContext.Users.Where(x => x.Id == combinationDto.Reader).FirstOrDefaultAsync();
            var combination = new Combination
            {
                Id = Guid.NewGuid(),
                Reader = reader,
                Author = combinationDto.Author,
                Year = combinationDto.Year,
                Genre = combinationDto.Genre,
                Publisher = combinationDto.Publisher,
                ShortDescription = combinationDto.ShortDescription,
                TempCombination = combinationDto.TempCombination,
                ResultPercentage = combinationDto.ResultPercentage,
                IsSuccessful = combinationDto.IsSuccessful
            };
            var result = await _unitOfWork.CombinationRepository.Add(combination);
            return result;
        }

        public async Task<Combination> Update(CombinationDto combinationDto)
        {
            try
            {
                var user = await _applicationContext.Users.Where(x => x.Id == combinationDto.Reader).FirstOrDefaultAsync();
                var combination = new Combination
                {
                    Id = combinationDto.Id,
                    Reader = user,
                    Author = combinationDto.Author,
                    Year = combinationDto.Year,
                    Genre = combinationDto.Genre,
                    Publisher = combinationDto.Publisher,
                    ShortDescription = combinationDto.ShortDescription,
                    TempCombination = combinationDto.TempCombination,
                    ResultPercentage = combinationDto.ResultPercentage,
                    IsSuccessful = combinationDto.IsSuccessful
                };
                
                await _unitOfWork.CombinationRepository.Update(combination);
                return combination;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Combination> DeleteById(Guid id)
        {
            var result = await _unitOfWork.CombinationRepository.DeleteById(id);
            return result;
        }
    }
}
