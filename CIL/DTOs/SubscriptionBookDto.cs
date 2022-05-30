using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class SubscriptionBookDto
    {
        public Guid Id { get; set; }
        public Guid Subscription { get; set; }
        public Guid Book { get; set; }
    }
}
