using System;

namespace ImageGalleryAndBlog.Data.Models
{
    public class ArticleComments
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Company { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}