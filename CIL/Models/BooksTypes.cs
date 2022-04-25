using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class BooksTypes
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public Type Type { get; set; }
        public double Price { get; set; }
    }
}
