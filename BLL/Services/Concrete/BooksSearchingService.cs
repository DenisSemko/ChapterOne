using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using DAL.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class BooksSearchingService : IBooksSearchingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public BooksSearchingService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Book>> FindBooks(Combination combination)
        {

            var shortDescription = _unitOfWork.CombinationRepository.ChangeShortDescriptionBehavior(combination.ShortDescription);
            var newCombination = new Combination
            {
                Id = combination.Id,
                Reader = combination.Reader,
                Author = combination.Author,
                Year = combination.Year,
                Genre = combination.Genre,
                Publisher = combination.Publisher,
                ShortDescription = shortDescription,
                IsSuccessful = combination.IsSuccessful
            };

            var booksByAuthor = await _applicationContext.Book.Where(x => x.Author == newCombination.Author).ToListAsync();
            var booksByYear = await _applicationContext.Book.Where(x => x.PublishedDate.Year == newCombination.Year).ToListAsync();
            var booksByGenre = await _applicationContext.Book.Where(x => x.Genre.Name == newCombination.Genre).ToListAsync();
            var booksByPublisher = await _applicationContext.Book.Where(x => x.Publisher == newCombination.Publisher).ToListAsync();
            var booksByDescription = await _applicationContext.Book.Where(x => x.ShortDescription.Contains(newCombination.ShortDescription)).ToListAsync();

            var findBooks = booksByAuthor.Intersect(booksByYear).Intersect(booksByGenre).Intersect(booksByPublisher).Intersect(booksByDescription);
            var findBooksSecond = booksByAuthor.Intersect(booksByYear).Intersect(booksByGenre);

            var combinationByFive = booksByAuthor
                .Select(x => new { x.Author, x.PublishedDate.Year, x.Genre, x.Publisher, x.ShortDescription })
                .Intersect(booksByYear.Select(x => new { x.Author, x.PublishedDate.Year, x.Genre, x.Publisher, x.ShortDescription }))
                .Intersect(booksByGenre.Select(x => new { x.Author, x.PublishedDate.Year, x.Genre, x.Publisher, x.ShortDescription }))
                .Intersect(booksByPublisher.Select(x => new { x.Author, x.PublishedDate.Year, x.Genre, x.Publisher, x.ShortDescription }))
                .Intersect(booksByDescription.Select(x => new { x.Author, x.PublishedDate.Year, x.Genre, x.Publisher, x.ShortDescription }))
                .ToList();
            var books = JsonSerializer.Deserialize<List<Book>>(JsonSerializer.Serialize(combinationByFive));

            return books;
        }


    }
}
