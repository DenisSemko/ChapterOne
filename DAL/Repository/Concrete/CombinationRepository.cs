using CIL.DTOs;
using CIL.Models;
using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                ShortDescription = shortDescription
            };
            return combination;
        }

        public string ChangeShortDescriptionBehavior(string shortDescription)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            shortDescription = shortDescription.Replace(",", "");
            shortDescription = shortDescription.Replace(".", "");
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

    }
}
