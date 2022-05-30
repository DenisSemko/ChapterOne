using BLL.Services.Abstract;
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
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationContext _myDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public StatisticService(ApplicationContext myDbContext, IUnitOfWork unitOfWork)
        {
            this._myDbContext = myDbContext;
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<Dictionary<string, double>> GetSubscriptionUser() 
        {
            var subscription = await _myDbContext.Subscription.ToListAsync();
            var usersSubscription = await _myDbContext.Users.Include(o => o.Subscription).ToListAsync();
            var data = new Dictionary<string, double>();
            var freeCounter = 0;
            var mediumCounter = 0;
            var highCounter = 0;

            foreach (var s in subscription)
            {
                foreach(var u in usersSubscription)
                {
                    if(u.Subscription != null)
                    {
                        switch (s.Name)
                        {
                            case "Free":
                                if (s.Id.Equals(u.Subscription.Id))
                                {
                                    freeCounter++;
                                    try
                                    {
                                        data.Add(s.Name, freeCounter);
                                    }
                                    catch (Exception)
                                    {
                                        data["Free"] = freeCounter;
                                    }
                                }
                                break;
                            case "Medium":
                                if (s.Id.Equals(u.Subscription.Id))
                                {
                                    mediumCounter++;
                                    try
                                    {
                                        data.Add(s.Name, mediumCounter);
                                    }
                                    catch (Exception)
                                    {
                                        data["Medium"] = mediumCounter;
                                    }
                                }
                                break;
                            case "High":
                                if (s.Id.Equals(u.Subscription.Id))
                                {
                                    highCounter++;
                                    try
                                    {
                                        data.Add(s.Name, highCounter);
                                    }
                                    catch (Exception)
                                    {
                                        data["High"] = highCounter;
                                    }


                                }
                                break;
                        }
                    }
                }
            }

            return data;
        }
        public async Task<Dictionary<string, double>> GetMostPopularBook()
        {
            var books = await _myDbContext.Book.ToListAsync();
            var data = new Dictionary<string, double>();
            foreach (var book in books)
            {
                var rate = await _unitOfWork.RateRepository.GetAverageMarkByBookId(book.Id);
                data.Add(book.Title, rate);
            }
            var result = data.OrderByDescending(x => x.Value).Take(3).ToDictionary(key => key.Key, value => value.Value);
            return result;
        }

    }
}
