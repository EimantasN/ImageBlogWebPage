using System;

namespace ImageGalleryAndBlog.Data.Models
{
    public class ArticleReference
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Publish { get; set; }
        public DateTime Date { get; set; }
    }
}