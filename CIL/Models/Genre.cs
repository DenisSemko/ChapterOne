using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CIL.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Book> Books { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Combination> Combinations { get; set; }

    }
}
