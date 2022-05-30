using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class BookPaymentDto
    {
        public Guid Id { get; set; }
        public Guid User { get; set; }
        public Guid Book { get; set; }
        public string Type { get; set; }
    }
}
