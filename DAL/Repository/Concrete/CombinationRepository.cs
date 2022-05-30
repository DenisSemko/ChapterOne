using CIL.DTOs;
using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concrete
{
    public class CombinationRepository : BaseRepository<Combination>, ICombinationRepository
    {
        public CombinationRepository(ApplicationContext myDbContext) : base(myDbContext) { }

        public async Task<Combination> GenerateCombination()
        {
            var bookToFindAuthor = await myDbContext.Book.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            var author = bookToFindAuthor.Author;
            if(author.Contains(","))
            {
                var words = author.Split(",");
                Random rand = new Random();
                var index = rand.Next(words.Length);
                author = words[index];
            }

            var bookToFindYear = await myDbContext.Book.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            var year = bookToFindYear.PublishedDate.Year;

            var bookToFindGenre = await myDbContext.Book.OrderBy(x => Guid.NewGuid()).Include(x => x.Genre).FirstOrDefaultAsync();
            var genre = bookToFindGenre.Genre.Name;

            var bookToFindPublisher = await myDbContext.Book.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            var publisher = bookToFindPublisher.Publisher;

            var bookToFindDescription = await myDbContext.Book.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            var shortDescription = ChangeShortDescriptionBehavior(bookToFindDescription.ShortDescription);
            var combination = new Combination
            {
                Id = Guid.NewGuid(),
                Author = author,
                Year = year,
                Genre = genre,
                Publisher = publisher,
                ShortDescription = shortDescription,
                IsSuccessful = false,
                TempCombination = "",
                ResultPercentage = 0
            };
            return combination;
        }

        public async Task<Combination> GenerateCombinationFromCollection(Guid userId)
        {
            try
            {
                var collections = await myDbContext.Collection.Where(o => o.User.Id == userId).Include(o => o.User).Include(o => o.Category).ToListAsync();
                var bookCollections = new List<BookCollection>();
                foreach (var collection in collections)
                {
                    var bookCollection = await myDbContext.BookCollection.Where(o => o.Collection.Id == collection.Id)
                    .Include(o => o.Book).Include(o => o.Book.Genre).Include(o => o.Collection).ToListAsync();
                    bookCollections.AddRange(bookCollection);
                }
                var books = bookCollections.Select(x => x.Book).Distinct().ToList();
                var bookToFindAuthor = books.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var author = bookToFindAuthor.Author;
                if (author.Contains(","))
                {
                    var words = author.Split(",");
                    Random rand = new Random();
                    var index = rand.Next(words.Length);
                    author = words[index];
                }

                var bookToFindYear = books.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var year = bookToFindYear.PublishedDate.Year;

                var bookToFindGenre = books.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var genre = bookToFindGenre.Genre.Name;

                var bookToFindPublisher = books.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var publisher = bookToFindPublisher.Publisher;

                var bookToFindDescription = books.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var shortDescription = ChangeShortDescriptionBehavior(bookToFindDescription.ShortDescription);
                var combination = new Combination
                {
                    Id = Guid.NewGuid(),
                    Author = author,
                    Year = year,
                    Genre = genre,
                    Publisher = publisher,
                    ShortDescription = shortDescription,
                    IsSuccessful = false,
                    TempCombination = "",
                    ResultPercentage = 0
                };
                return combination;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ChangeShortDescriptionBehavior(string shortDescription)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            shortDescription = shortDescription.Replace(",", "");
            shortDescription = shortDescription.Replace(".", "");
            shortDescription = shortDescription.Replace("their", "");
            shortDescription = shortDescription.Replace("this", "");
            shortDescription = shortDescription.Replace("that", "");
            shortDescription = shortDescription.Replace("with", "");
            shortDescription = shortDescription.Replace("those", "");
            shortDescription = shortDescription.Replace("because", "");
            shortDescription = shortDescription.Replace("only", "");
            shortDescription = shortDescription.Replace("from", "");
            shortDescription = shortDescription.Replace("before", "");
            shortDescription = shortDescription.Replace("after", "");
            shortDescription = shortDescription.Replace("After", "");
            shortDescription = shortDescription.Replace("they", "");
            shortDescription = shortDescription.Replace("whose", "");
            shortDescription = shortDescription.Replace("over", "");
            shortDescription = shortDescription.Replace("more", "");
            string[] arr = shortDescription.Split(' ');

            foreach (string word in arr) 
            {
                if (word.Length >= 4) 
                {
                    if (dictionary.ContainsKey(word))
                        dictionary[word] = dictionary[word] + 1;
                    else
                        dictionary[word] = 1;
                }
            }
            var mostFrequent = dictionary.OrderByDescending(c => c.Value).First().Key;
            return mostFrequent;
        }

        public async Task<IEnumerable<Combination>> LoadOldSchemes(Guid currentUser)
        {
            var combinations = await GetByUser(currentUser);
            var filteredBestCombinations = combinations.Where(x => x.IsSuccessful == true).OrderByDescending(x => x.ResultPercentage).ToList();
            return filteredBestCombinations;
        }
        public async Task<IEnumerable<Combination>> GetByUser(Guid userId)
        {
            var combinations = await myDbContext.Combination.Where(x => x.Reader.Id == userId).ToListAsync();
            return combinations;
        }
    }
}
