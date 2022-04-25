using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.DTOs
{
    public class BookAddDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid Genre { get; set; }
        public string Language { get; set; }
        public string ISBN { get; set; }
        public string ShortDescription { get; set; }
        public int ReadingAge { get; set; }
        public Guid Image { get; set; }
        public Guid File { get; set; }
        public bool IsQualified { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
