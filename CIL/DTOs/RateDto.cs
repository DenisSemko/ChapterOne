using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class RateDto
    {
        public Guid Id { get; set; }
        public double Mark { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Guid Book { get; set; }
        public Guid User { get; set; }
    }
}
