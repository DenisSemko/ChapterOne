using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class BooksTypesDto
    {
        public Guid Id { get; set; }
        public Guid Book { get; set; }
        public Guid Type { get; set; }
        public double Price { get; set; }
    }
}
