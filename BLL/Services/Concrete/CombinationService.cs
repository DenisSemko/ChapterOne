using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class CombinationService : ICombinationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CombinationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Combination> GenerateCombination()
        {
            var result = await _unitOfWork.CombinationRepository.GenerateCombination();
            return result;
        }

        public async Task<Combination> Add(Combination combination)
        {
            var result = await _unitOfWork.CombinationRepository.Add(combination);
            return result;
        }
        public async Task<Combination> DeleteById(Guid id)
        {
            var result = await _unitOfWork.CombinationRepository.DeleteById(id);
            return result;
        }
    }
}
