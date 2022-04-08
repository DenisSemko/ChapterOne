using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CIL.Models
{
    public class BookImage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Book> Books { get; set; }
    }
}
