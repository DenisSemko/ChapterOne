using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class CollectionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid User { get; set; }
        public Guid Category { get; set; }
    }
}
