using BLL.Services.Abstract;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class StatisticService : IStatisticService
    {
        private ApplicationContext _myDbContext;

        public StatisticService(ApplicationContext myDbContext)
        {
            this._myDbContext = myDbContext;
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
    }
}
