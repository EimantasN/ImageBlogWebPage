using System;
using System.Collections.Generic;

namespace ImageGalleryAndBlog.Data.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public string ImgUrl { get; set; }
        public string SubText { get; set; }
        public string Category { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public virtual IEnumerable<ArticleTags> ArticleTags { get; set; }
        public virtual IEnumerable<ArticleComments> ArticleComments { get; set; }
        public virtual IEnumerable<ArticleReference> ArticleReference { get; set; }
    }
}