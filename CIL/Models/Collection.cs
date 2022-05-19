using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CIL.Models
{
    public class Collection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<BookCollection> BookCollections { get; set; }
    }
}
