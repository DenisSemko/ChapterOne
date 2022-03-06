using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IStatisticService
    {
        public Task<Dictionary<string, double>> GetSubscriptionUser();
    }
}
