using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ImageGalleryAndBlog.Data.Models;

namespace ImageGalleryAndBlog.Data
{
    public class ImageGalleryAndBlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public ImageGalleryAndBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<ImageTags> ImageTags { get; set; }
        public DbSet<ImageDistinctCategorys> ImageDistinctCategorys { get; set; }

        public DbSet<Article> Article { get; set; }
        public DbSet<Models.ArticleReference> ArticleReferences { get; set; }
        public DbSet<ArticleComments> ArticleComments { get; set; }
        public DbSet<ArticleTags> ArticleTags { get; set; }

        public DbSet<ArticleDistinctCategorys> ArticleDistinctCategorys { get; set; }
        public DbSet<ArticleDistinctTags> ArticleDistinctTags { get; set; }


        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<CheckSpam> CheckSpams { get; set; }
        public DbSet<Visitors> Visitors { get; set; }
    }
}