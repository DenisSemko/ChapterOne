using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public Guid User { get; set; }
        public string Name { get; set; }
    }
}
