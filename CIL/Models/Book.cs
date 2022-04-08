using System;
using System.Collections.Generic;
using System.Text;

namespace CIL.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public Genre Genre { get; set; }
        public string Language { get; set; }
        public string ISBN { get; set; }
        public string ShortDescription { get; set; }
        public int ReadingAge { get; set; }
        public BookImage Image { get; set; }
        public BookFile File { get; set; }
        public bool IsQualified { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
