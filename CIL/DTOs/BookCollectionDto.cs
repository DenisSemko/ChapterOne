using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class BookCollectionDto
    {
        public Guid Id { get; set; }
        public Guid Book { get; set; }
        public Guid Collection { get; set; }
    }
}
