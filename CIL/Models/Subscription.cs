using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CIL.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public double Price {  get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<User> UserSubscription { get; set; }
    }
}
