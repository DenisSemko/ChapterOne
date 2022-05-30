using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class BookPayment
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public string Type { get; set; }
    }
}
