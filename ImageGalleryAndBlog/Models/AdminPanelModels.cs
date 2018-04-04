using ImageGalleryAndBlog.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageGalleryAndBlog.Models
{
    public class IndexModel
    {
        public IEnumerable<Visitors> Visitors { get; set; }
    }

    public class ImageInsert
    {
        public string Category { get; set; }
        public string Tags { get; set; }
        public string Url { get; set; }
    }

    public class ImageDelete
    {
        [Required]
        public string DeleteUrl { get; set; }
    }

    public class ImageUpdate
    {
        [Required]
        public string OldUrl { get; set; }

        public string Category { get; set; }
        public string Tags { get; set; }
    }

    public class ArticleInsert
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string SubText { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public string Category { get; set; }

        public string Tags { get; set; }
    }

    public class ArticleDelete
    {
        [Required]
        public string Title { get; set; }
    }

    public class ArticleUpdate
    {
        [Required]
        public string OldTitle { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string SubText { get; set; }
        public string ImgUrl { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
    }

    public class ArticleReference
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Publish { get; set; }

        public DateTime Date { get; set; }
    }

    public class TableModel
    {
        public IEnumerable<GalleryImage> Images { get; set; }
        public IEnumerable<ImageTags> ImageTags { get; set; }

        public IEnumerable<Article> Article { get; set; }
        public IEnumerable<ArticleTags> ArticleTags { get; set; }
        public IEnumerable<ArticleComments> ArticleComment { get; set; }
        public IEnumerable<ArticleReference> ArticleReferences { get; set; }

        public IEnumerable<Subscribers> Subscribers { get; set; }
    }
}