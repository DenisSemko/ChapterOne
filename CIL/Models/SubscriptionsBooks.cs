using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class SubscriptionsBooks
    {
        public Guid Id { get; set; }
        public Subscription Subscription { get; set; }
        public Book Book { get; set; }
    }
}
