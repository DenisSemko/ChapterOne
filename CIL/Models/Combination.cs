using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class Combination
    {
        public Guid Id { get; set; }
        public User Reader { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string ShortDescription { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
