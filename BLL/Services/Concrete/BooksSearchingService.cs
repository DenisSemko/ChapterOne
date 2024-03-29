﻿using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using DAL.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<string> _schemesAttributes;

        public BooksSearchingService(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
            this._schemesAttributes = new List<string>();
        }

        public async Task<IEnumerable<Book>> FindBooks(CombinationDto combinationDto)
        {
            var booksByAuthor = await _applicationContext.Book.Where(x => x.Author == combinationDto.Author).ToListAsync();
            var booksByYear = await _applicationContext.Book.Where(x => x.PublishedDate.Year == combinationDto.Year).ToListAsync();
            var booksByGenre = await _applicationContext.Book.Where(x => x.Genre.Name == combinationDto.Genre).ToListAsync();
            var booksByPublisher = await _applicationContext.Book.Where(x => x.Publisher == combinationDto.Publisher).ToListAsync();
            var booksByDescription = await _applicationContext.Book.Where(x => x.ShortDescription.Contains(combinationDto.ShortDescription)).ToListAsync();

            CheckSchemesAttributes(booksByAuthor, booksByYear, booksByGenre, booksByPublisher, booksByDescription);

            var findBooks = new List<Book>();
            var schemes = SchemesGeneration(_schemesAttributes).ToList();

            while(findBooks.Count() == 0)
            {
                var schemesByFive = schemes.Where(x => x.Count() == 5).FirstOrDefault();
                if(schemesByFive != null)
                {
                    var findBooksByFive = GetIntersectionByFive(booksByAuthor, booksByYear, booksByGenre, booksByPublisher, booksByDescription).ToList();
                    if(findBooksByFive.Count() > 0)
                    {
                        findBooks.AddRange(findBooksByFive);
                        break;
                    }
                }
                var schemesByFour = schemes.Where(x => x.Count() == 4).ToList();
                if (schemesByFour.Count() > 0)
                {
                    var findBooksByFour = GetSchemeByFour(booksByAuthor, booksByYear, booksByGenre, booksByPublisher, booksByDescription, combinationDto).ToList();
                    if (findBooksByFour.Count() > 0)
                    {
                        findBooks.AddRange(findBooksByFour);
                        break;
                    }
                }
                var schemesByThree = schemes.Where(x => x.Count() == 3).ToList();
                if (schemesByThree.Count() > 0)
                {
                    var findBooksByThree = GetSchemeByThree(booksByAuthor, booksByYear, booksByGenre, booksByPublisher, booksByDescription, combinationDto).ToList();
                    if (findBooksByThree.Count() > 0)
                    {
                        findBooks.AddRange(findBooksByThree);
                        break;
                    }
                }
                var schemesByTwo = schemes.Where(x => x.Count() == 2).ToList();
                if (schemesByTwo.Count() > 0)
                {
                    var findBooksByTwo = GetSchemeByTwo(booksByAuthor, booksByYear, booksByGenre, booksByPublisher, booksByDescription, combinationDto).ToList();
                    if (findBooksByTwo.Count() > 0)
                    {
                        findBooks.AddRange(findBooksByTwo);
                        break;
                    }
                }
                var oneAuthor = booksByAuthor;
                var oneYear = booksByYear;
                var oneGenre = booksByGenre;
                var onePublisher = booksByPublisher;
                var oneWord = booksByDescription;
                if (oneAuthor.Count() > 0)
                {
                    findBooks.AddRange(oneAuthor);
                    break;
                }
                else if (oneYear.Count() > 0)
                {
                    findBooks.AddRange(oneYear);
                    break;
                }
                else if (oneGenre.Count() > 0)
                {
                    findBooks.AddRange(oneGenre);
                    break;
                }
                else if (onePublisher.Count() > 0)
                {
                    findBooks.AddRange(onePublisher);
                    break;
                }
                else if (oneWord.Count() > 0)
                {
                    findBooks.AddRange(oneWord);
                    break;
                }

                break;
            }
            
            return findBooks;
        }

        private void CheckSchemesAttributes(List<Book> booksByAuthor, List<Book> booksByYear, List<Book> booksByGenre, List<Book> booksByPublisher, List<Book> booksByDescription)
        {
            if(booksByAuthor.Count() >= 1)
            {
                _schemesAttributes.Add("Author");
            }
            if (booksByYear.Count() >= 1)
            {
                _schemesAttributes.Add("Year");
            }
            if (booksByGenre.Count() >= 1)
            {
                _schemesAttributes.Add("Genre");
            }
            if (booksByPublisher.Count() >= 1)
            {
                _schemesAttributes.Add("Publisher");
            }
            if (booksByDescription.Count() >= 1)
            {
                _schemesAttributes.Add("ShortDescription");
            }
        }

        private IEnumerable<T[]> SchemesGeneration<T>(IEnumerable<T> source)
        {
            if (null == source)
                throw new ArgumentNullException(nameof(source));

            T[] data = source.ToArray();

            return Enumerable
              .Range(1, 1 << (data.Length))
              .Select(index => data
                 .Where((v, i) => (index & (1 << i)) != 0)
                 .ToArray());
        }

        private IEnumerable<T> GetIntersectionByFive<T>(IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third, IEnumerable<T> fourth, IEnumerable<T> fifth)
        {
            var result = first.Intersect(second).Intersect(third).Intersect(fourth).Intersect(fifth);
            return result;
        }

        private IEnumerable<T> IntersectByFour<T>(IEnumerable<T> first, IEnumerable<T> second, IEnumerable<T> third, IEnumerable<T> fourth)
        {
            var result = first.Intersect(second).Intersect(third).Intersect(fourth);
            return result;
        }

        private List<Book> GetSchemeByFour(List<Book> booksByAuthor, List<Book> booksByYear, List<Book> booksByGenre, List<Book> booksByPublisher, List<Book> booksByDescription, CombinationDto temp)
        {
            var findBooksByFour = new List<Book>();
            while (findBooksByFour.Count() == 0)
            {
                var findBooksByFourFirst = IntersectByFour(booksByAuthor, booksByYear, booksByGenre, booksByPublisher).ToList();
                if(findBooksByFourFirst.Count() > 0)
                {
                    findBooksByFour.AddRange(findBooksByFourFirst);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year", "Genre", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByFourSecond = booksByAuthor.Intersect(booksByYear).Intersect(booksByGenre).Intersect(booksByDescription).ToList();
                if (findBooksByFourSecond.Count() > 0)
                {
                    findBooksByFour.AddRange(findBooksByFourSecond);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year", "Genre", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByFourThird = booksByAuthor.Intersect(booksByYear).Intersect(booksByPublisher).Intersect(booksByDescription).ToList();
                if (findBooksByFourThird.Count() > 0)
                {
                    findBooksByFour.AddRange(findBooksByFourThird);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year", "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByFourFourth = booksByAuthor.Intersect(booksByGenre).Intersect(booksByPublisher).Intersect(booksByDescription).ToList();
                if (findBooksByFourFourth.Count() > 0)
                {
                    findBooksByFour.AddRange(findBooksByFourFourth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Genre", "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByFourFifth = booksByYear.Intersect(booksByGenre).Intersect(booksByPublisher).Intersect(booksByDescription).ToList();
                if (findBooksByFourFifth.Count() > 0)
                {
                    findBooksByFour.AddRange(findBooksByFourFifth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "Genre", "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                break;
            };
            var result = findBooksByFour;
            return result;
        }

        private List<Book> GetSchemeByThree(List<Book> booksByAuthor, List<Book> booksByYear, List<Book> booksByGenre, List<Book> booksByPublisher, List<Book> booksByDescription, CombinationDto temp)
        {
            var findBooksByThree = new List<Book>();
            while (findBooksByThree.Count() == 0)
            {
                var findBooksByThreeFirst = booksByAuthor.Intersect(booksByYear).Intersect(booksByGenre).ToList();
                if (findBooksByThreeFirst.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeFirst);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year", "Genre" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeSecond = booksByAuthor.Intersect(booksByYear).Intersect(booksByPublisher).ToList();
                if (findBooksByThreeSecond.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeSecond);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeThird = booksByAuthor.Intersect(booksByYear).Intersect(booksByDescription).ToList();
                if (findBooksByThreeThird.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeThird);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeFourth = booksByYear.Intersect(booksByGenre).Intersect(booksByPublisher).ToList();
                if (findBooksByThreeFourth.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeFourth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "Genre", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeFifth = booksByYear.Intersect(booksByGenre).Intersect(booksByDescription).ToList();
                if (findBooksByThreeFifth.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeFifth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "Genre", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeSixth = booksByGenre.Intersect(booksByPublisher).Intersect(booksByDescription).ToList();
                if (findBooksByThreeSixth.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeSixth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Genre", "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeSeventh = booksByAuthor.Intersect(booksByGenre).Intersect(booksByPublisher).ToList();
                if (findBooksByThreeSeventh.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeSeventh);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Genre", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeEigth = booksByAuthor.Intersect(booksByPublisher).Intersect(booksByDescription).ToList();
                if (findBooksByThreeEigth.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeEigth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeNinth = booksByYear.Intersect(booksByPublisher).Intersect(booksByDescription).ToList();
                if (findBooksByThreeNinth.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeNinth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByThreeTenth = booksByAuthor.Intersect(booksByGenre).Intersect(booksByDescription).ToList();
                if (findBooksByThreeTenth.Count() > 0)
                {
                    findBooksByThree.AddRange(findBooksByThreeTenth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Genre", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                break;
            };
            var result = findBooksByThree;
            return result;
        }

        private List<Book> GetSchemeByTwo(List<Book> booksByAuthor, List<Book> booksByYear, List<Book> booksByGenre, List<Book> booksByPublisher, List<Book> booksByDescription, CombinationDto temp)
        {
            var findBooksByTwo = new List<Book>();
            while (findBooksByTwo.Count() == 0)
            {
                var findBooksByTwoFirst = booksByAuthor.Intersect(booksByYear).ToList();
                if (findBooksByTwoFirst.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoFirst);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Year" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoSecond = booksByAuthor.Intersect(booksByGenre).ToList();
                if (findBooksByTwoSecond.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoSecond);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Genre" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoThird = booksByYear.Intersect(booksByGenre).ToList();
                if (findBooksByTwoThird.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoThird);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "Genre" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoFourth = booksByAuthor.Intersect(booksByPublisher).ToList();
                if (findBooksByTwoFourth.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoFourth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoFifth = booksByYear.Intersect(booksByPublisher).ToList();
                if (findBooksByTwoFifth.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoFifth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoSixth = booksByGenre.Intersect(booksByPublisher).ToList();
                if (findBooksByTwoSixth.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoSixth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Genre", "Publisher" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoSeventh = booksByAuthor.Intersect(booksByDescription).ToList();
                if (findBooksByTwoSeventh.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoSeventh);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Author", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoEigth = booksByYear.Intersect(booksByDescription).ToList();
                if (findBooksByTwoEigth.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoEigth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Year", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoNinth = booksByGenre.Intersect(booksByDescription).ToList();
                if (findBooksByTwoNinth.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoNinth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Genre", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                var findBooksByTwoTenth = booksByPublisher.Intersect(booksByDescription).ToList();
                if (findBooksByTwoTenth.Count() > 0)
                {
                    findBooksByTwo.AddRange(findBooksByTwoTenth);
                    var scheme = SchemesGeneration(_schemesAttributes)
                        .Where(x => x.SequenceEqual(new string[] { "Publisher", "ShortDescription" })).FirstOrDefault();
                    temp.TempCombination = string.Join(" ", scheme);
                    break;
                }
                break;
            };
            var result = findBooksByTwo;
            return result;
        }


    }
}
