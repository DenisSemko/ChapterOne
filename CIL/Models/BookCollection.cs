using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class BookCollection
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public Collection Collection { get; set; }
    }
}
