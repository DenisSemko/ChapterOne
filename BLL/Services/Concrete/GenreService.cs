using BLL.Services.Abstract;
using CIL.Models;
using DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Services.Concrete
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Genre>> Get()
        {
            return await _unitOfWork.GenreRepository.Get();
        }

        public async Task<Genre> Add(Genre genre)
        {
            var result = await _unitOfWork.GenreRepository.Add(genre);
            return result;
        }
    }
}
