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
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public CollectionService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Collection>> GetByUserId(Guid id)
        {
            var result = await _unitOfWork.CollectionRepository.GetByUserId(id);
            return result;
        }

        public async Task<Collection> Add(CollectionDto collectionDto)
        {
            try
            {
                var user = await _applicationContext.Users.Where(x => x.Id == collectionDto.User).FirstOrDefaultAsync();
                var category = await _applicationContext.Category.Where(x => x.Id == collectionDto.Category).FirstOrDefaultAsync();
                var collection = new Collection
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Name = collectionDto.Name,
                    Category = category
                };
                await _unitOfWork.CollectionRepository.Add(collection);
                return collection;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Collection> DeleteById(Guid id)
        {
            var result = await _unitOfWork.CollectionRepository.DeleteById(id);
            return result;
        }
    }
}
