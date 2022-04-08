using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace CIL.Models
{
    public class BookFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AudioFile { get; set; }
        public string WebFile { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Book> Books { get; set; }
    }
}
