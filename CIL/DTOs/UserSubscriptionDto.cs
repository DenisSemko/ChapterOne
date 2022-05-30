using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class UserSubscriptionDto
    {
        public string Username { get; set; }
        public DateTime TimeSubscriptionPaid { get; set; }
        public bool IsSubscriptionPaid { get; set; }
    }
}
