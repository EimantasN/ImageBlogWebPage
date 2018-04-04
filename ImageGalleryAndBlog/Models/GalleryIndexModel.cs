using ImageGalleryAndBlog.Data.Models;
using System.Collections.Generic;

namespace ImageGalleryAndBlog.Models
{

    public class KategorijosModel
    {
        public string[] Kategorijos { get; set; }
    }

    public class GalleryIndexModel
    {
        public string[] Kat;
        public IEnumerable<GalleryImage> Images { get; set; }
        public string SearchQuery { get; set; }
        public int Id { get; set; }

        public string GetTags(GalleryImage image)
        {
            string tags = "";
            foreach (ImageTags temp in image.Tags)
            {
                tags = tags + temp.Description + " ";
            }

            return tags;
        }
    }

    public class BlogModel
    {
        public string[] Kat;
        public IEnumerable<Article> Article { get; set; }
        public string[] Articletags { get; set; }
        public string[] ArticleCategorys { get; set; }

        public int Page { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }

        public string email { get; set; }

        //public int Skaiciuoti()
        //{
        //    return Article.Count();
        //}

        ////public bool activePage(int i)
        ////{
        ////    if (i == Page)
        ////        return true;
        ////    return false;
        ////}

        //public int Limit()
        //{
        //    if (Article.Count() % 5 == 0)
        //        return Article.Count() / 5;
        //    return Article.Count() / 5 + 1;
        //}

        public string GetTags(Article arc)
        {
            string tags = "";
            foreach (ArticleTags temp in arc.ArticleTags)
            {
                tags = tags + temp.Description + " ";
            }

            return tags;
        }
    }

    public class ArticleModel
    {
        public Article Article { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
    }

    public class AboutModel
    {
        public string[] Kat;
        public IEnumerable<GalleryImage> Images { get; set; }
    }

    public class ContactModel
    {
        public string[] Kat;
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Mess { get; set; }
    }

    public class Send
    {
        public string[] Kat;

        public string Header { get; set; }
        public string Message { get; set; }
    }
}