using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CIL.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Collection> Collections { get; set; }
    }
}
