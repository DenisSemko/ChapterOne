using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class Rate
    {
        public Guid Id { get; set; }
        public double Mark { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
