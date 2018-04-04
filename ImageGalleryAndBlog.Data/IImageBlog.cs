using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGalleryAndBlog.Data.Models;

namespace ImageGalleryAndBlog.Data
{
    public interface IImageBlog
    {
        IEnumerable<GalleryImage> GetAllImages();

        IEnumerable<ImageTags> GetImageTags();

        IEnumerable<GalleryImage> GetImagesWithTag(string tag);

        IEnumerable<GalleryImage> OneImageFormEveryCategory();

        IEnumerable<GalleryImage> GetImagesByCategory(string category);

        IEnumerable<ImageTags> ParseImageTags(string tags);

        //IMAGE
        Task ImageInsert(string title, string tags, string url);

        Task DeleteImageByUrl(string deleteUrl);

        Task ImageUpdateByUlr(string oldUrl, string category, string tags);




        IEnumerable<Article> GetAllArticle(string category, string tag);
        Article GetArticleById(int Id);

        IEnumerable<ArticleTags> GetArticleTags();
        IEnumerable<ArticleReference> GetArticleReferences();
        IEnumerable<ArticleComments> GeArticleComments();

        IEnumerable<ArticleTags> ParseArticleTags(string tags);



        //ARTICLE
        string[] GetDistinctArticleTags();
        string[] GetDistinctArticleCategorys();

        Task ArticleInsert(string title, string author, string miniText, string text, string imgUrl, string category,
            string tags);

        Task ArticleDelete(string title);

        Task ArticleUpdate(string oldTitle, string title, string author, string subText, string text, string imgUrl,
            string category, string tags);

        Task AddComment(string firstName, string lastName, string email, string message, int Id);

        //MAIN PAGE NAVIGATION
        string[] Kategorijos();

        //SUBSCIBER
        Task AddSubscriber(string email);
        IEnumerable<Subscribers> GetSubscriberses();

        IEnumerable<Visitors> GetVisitors();
        Task AddVisitor(string ip);
        Task<bool> CheckSpam(string ip);

    }
}