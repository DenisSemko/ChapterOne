using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class CombinationDto
    {
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public string ShortDescription { get; set; }
        public string TempCombination { get; set; }
        public int? ResultPercentage { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
