using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}
