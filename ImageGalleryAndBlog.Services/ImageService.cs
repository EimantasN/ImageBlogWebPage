using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageGalleryAndBlog.Data;
using ImageGalleryAndBlog.Data.Models;

namespace ImageGalleryAndBlog.Services
{
    public class ImageService : IImageBlog
    {
        //Prisijungimas prie SQL database(nzn visko gerai, bet cia conenction yra užmesgamas)
        private readonly ImageGalleryAndBlogDbContext _ctx;

        public ImageService(ImageGalleryAndBlogDbContext ctx)
        {
            _ctx = ctx;
        }

        //Grazina Kategorijas
        public string[] Kategorijos()
        {
            return _ctx.ImageDistinctCategorys.Select(x => x.Category).ToArray();
        }

        //Gražina visus paveikslėlius
        public IEnumerable<GalleryImage> GetAllImages()
        {
            return _ctx.GalleryImages.Include(x => x.Tags);
        }

        public IEnumerable<ImageTags> GetImageTags()
        {
            return _ctx.ImageTags;
        }

        public IEnumerable<GalleryImage> OneImageFormEveryCategory()
        {
            var images = new List<GalleryImage>();
            foreach (var a in Kategorijos())
                images.Add(_ctx.GalleryImages.First(img => img.Category == a));

            return images;
        }

        //Gauna paveikslėlius pagal TAGĄ
        public IEnumerable<GalleryImage> GetImagesWithTag(string tag)
        {
            return _ctx.GalleryImages.Include(x => x.Tags).Where(
                img => img.Tags.Any
                    (t => t.Description == tag));
        }

        //Grazina paveiksliukus tik tos pasirinktos kategorijos
        public IEnumerable<GalleryImage> GetImagesByCategory(string category)
        {
            return _ctx.GalleryImages.Include(x => x.Tags).Where(x => x.Category == category);
        }

        //Metodas ideda paveikslėlį(BE STORAGE)
        public async Task ImageInsert(string category, string tags, string url)
        {
            var image = new GalleryImage
            {
                Category = category,
                Tags = ParseImageTags(tags),
                Url = url,
                Created = DateTime.Now
            };
            _ctx.Add(image);
            await _ctx.SaveChangesAsync();

            await UpdateDistintImageCategory();
        }

        //Metodas kuris istrina jau esama Paveiksleli pagal URL
        public async Task DeleteImageByUrl(string deleteUrl)
        {
            try
            {
                var img = _ctx.GalleryImages.First(x => x.Url == deleteUrl);

                _ctx.Database.ExecuteSqlCommand("Delete From ImageTags Where GalleryImageId = " + img.Id);
                _ctx.Database.ExecuteSqlCommand("Delete From GalleryImages Where Id = " + img.Id);
                await _ctx.SaveChangesAsync();

            }
            catch
            {
            }

            await UpdateDistintImageCategory();
        }


        //UPDATE KAZKO NEVEIKIA
        public async Task ImageUpdateByUlr(string oldUrl, string category, string tags)
        {
            var temp = GetAllImages().First(x => x.Url == oldUrl);

            if (temp != null)
            {
                IEnumerable<ImageTags> newtags = null;

                if (category.Length <= 0)
                {
                    category = temp.Category;
                }
                newtags = tags.Length <= 0 ? temp.Tags : ParseImageTags(tags);

                var image = new GalleryImage
                {
                    Category = category,
                    Tags = newtags,
                    Url = oldUrl,
                    Created = DateTime.Now
                };

                _ctx.Update(image);
                await _ctx.SaveChangesAsync();
            }

            await UpdateDistintImageCategory();
        }

        //Gets distint tags from article database
        public string[] GetDistinctArticleTags()
        {
            return _ctx.ArticleDistinctTags.Select(x => x.Tag).ToArray();
        }
        //Gets distint tags from article database
        public string[] GetDistinctArticleCategorys()
        {
            return _ctx.ArticleDistinctCategorys.Select(x => x.Category).ToArray();
        }

        //Gražina visus Straipsnius
        public IEnumerable<Article> GetAllArticle(string category, string tag)
        {
            if (category != null)
                return _ctx.Article.Include(x => x.ArticleTags).Where(x => x.Category == category);
            if (tag != null)
                return _ctx.Article.Include(x => x.ArticleTags).Where(
                    arc => arc.ArticleTags.Any
                        (t => t.Description == tag));
            return _ctx.Article.Include(x => x.ArticleTags);
        }

        public Article GetArticleById(int Id)
        {
            return _ctx.Article
                    .Include(tag => tag.ArticleTags)
                    .Include(com => com.ArticleComments)
                    .First(x => x.Id == Id);
        }

        public IEnumerable<ArticleTags> GetArticleTags()
        {
            return _ctx.ArticleTags;
        }

        public IEnumerable<ArticleReference> GetArticleReferences()
        {
            return _ctx.ArticleReferences;
        }

        public IEnumerable<ArticleComments> GeArticleComments()
        {
            return _ctx.ArticleComments;
        }

        public async Task ArticleInsert(string title, string author, string miniText, string text, string imgUrl,
            string category, string tags)
        {
            var article = new Article
            {
                Title = title,
                Author = author,
                SubText = miniText,
                Text = text,
                ImgUrl = imgUrl,
                Category = category,
                ArticleTags = ParseArticleTags(tags),
                Created = DateTime.Now
            };
            _ctx.Add(article);
            await _ctx.SaveChangesAsync();

            await UpdateDistintArticleCategory();
            await UpdateDistintArticleTags();
        }

        public async Task ArticleDelete(string title)
        {
            try
            {
                var arc = _ctx.Article.First(x => x.Title == title);

                _ctx.Database.ExecuteSqlCommand("Delete From ArticleReference Where ArticleId = " + arc.Id);
                _ctx.Database.ExecuteSqlCommand("Delete From ArticleComments Where ArticleId = " + arc.Id);
                _ctx.Database.ExecuteSqlCommand("Delete From ArticleTags Where ArticleId = " + arc.Id);
                _ctx.Database.ExecuteSqlCommand("Delete From Article Where Id = " + arc.Id);
                await _ctx.SaveChangesAsync();
            }
            catch
            {
            }

            await UpdateDistintArticleCategory();
            await UpdateDistintArticleTags();
        }

        public async Task ArticleUpdate(string oldTitle, string title, string author, string subText, string text,
            string imgUrl, string category, string tags)
        {
            var temp = GetAllArticle(null, null).First(x => x.Title == oldTitle);

            if (temp != null)
            {
                IEnumerable<ArticleTags> newtags = null;

                if (title == null) title = temp.Title;
                if (author.Length <= 0) author = temp.Author;
                if (subText.Length <= 0) subText = temp.SubText;
                if (text.Length <= 0) text = temp.Text;
                if (imgUrl.Length <= 0) imgUrl = temp.ImgUrl;
                if (category.Length <= 0) category = temp.Category;

                newtags = tags.Length <= 0 ? temp.ArticleTags : ParseArticleTags(tags);

                var article = new Article
                {
                    Title = title,
                    Author = author,
                    SubText = subText,
                    Text = text,
                    ImgUrl = imgUrl,
                    Category = category,
                    ArticleTags = newtags,
                    Created = DateTime.Now
                };

                _ctx.Remove(temp);
                _ctx.Add(article);
                await _ctx.SaveChangesAsync();
            }

            await UpdateDistintArticleCategory();
            await UpdateDistintArticleTags();
        }

        public async Task AddComment(string firstName, string lastName, string email, string message, int id)
        {
            var com = new ArticleComments
            {
                Author = firstName + " " + lastName,
                Company = email,
                Created = DateTime.Now,
                Dislikes = 0,
                Likes = 0,
                Text = message
            };
            var arc = _ctx.Article.First(x => x.Id == id);

            var list = new List<ArticleComments> { com };

            if (arc.ArticleComments != null)
            {
                list.AddRange(arc.ArticleComments);
            }
            IEnumerable<ArticleComments> allCom = list;
            arc.ArticleComments = allCom;

            _ctx.Update(arc);
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<ArticleTags> ParseArticleTags(string tags)
        {
            var articleTags = new List<ArticleTags>();

            if (tags.Length > 0)
            {
                var tagList = tags.Split(",").ToList();

                foreach (var tag in tagList)
                    articleTags.Add(new ArticleTags
                    {
                        Description = tag.Trim()
                    });
            }
            else
            {
                articleTags.Add(new ArticleTags
                {
                    Description = "Ieva Photography"
                });
            }

            return articleTags;
        }

        public IEnumerable<ImageTags> ParseImageTags(string tags)
        {
            var imageTags = new List<ImageTags>();
            try
            {
                var tagList = tags.Split(",").ToList();

                foreach (var tag in tagList)
                    imageTags.Add(new ImageTags
                    {
                        Description = tag.Trim()
                    });
            }
            catch
            {
                imageTags.Add(new ImageTags
                {
                    Description = "Ieva Photography"
                });
            }

            return imageTags;
        }


        //WEB FAST MEMORY, For faster page load
        public async Task UpdateDistintImageCategory()
        {
            var all = _ctx.ImageDistinctCategorys;
            _ctx.RemoveRange(all);

            foreach (string category in _ctx.GalleryImages.Select(x => x.Category).Distinct().ToArray())
            {
                var distinctCategory = new ImageDistinctCategorys
                {
                    Category = category
                };
                _ctx.Add(distinctCategory);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task UpdateDistintArticleCategory()
        {
            var all = _ctx.ArticleDistinctCategorys;
            _ctx.RemoveRange(all);

            foreach (string category in _ctx.Article.Select(x => x.Category).Distinct().ToArray())
            {
                var distinctCategory = new ArticleDistinctCategorys
                {
                    Category = category
                };
                _ctx.Add(distinctCategory);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task UpdateDistintArticleTags()
        {
            var all = _ctx.ArticleDistinctTags;
            _ctx.RemoveRange(all);

            foreach (string tag in _ctx.ArticleTags.Select(x => x.Description).Distinct().ToArray())
            {
                var distinctTags = new ArticleDistinctTags
                {
                    Tag = tag
                };
                _ctx.Add(distinctTags);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task AddSubscriber(string email)
        {
            var subscriber = new Subscribers
            {
                Email = email
            };
            if (email.Contains("@") && email.Length > 5 && !_ctx.Subscribers.Select(x => x.Email).ToList().Contains(email))
            {
                if (!_ctx.Subscribers.Select(x => x.Email).ToList().Contains(email))
                {
                    _ctx.Add(subscriber);
                    await _ctx.SaveChangesAsync();
                }

            }
        }

        public IEnumerable<Subscribers> GetSubscriberses()
        {
            return _ctx.Subscribers;
        }

        public async Task AddVisitor(string ip)
        {
            var visitor = new Visitors
            {
                Ip = ip,
                Created = DateTime.Now
            };
            _ctx.Add(visitor);
            await _ctx.SaveChangesAsync();

        }

        public IEnumerable<Visitors> GetVisitors()
        {
            return _ctx.Visitors.Where(date => date.Created.Month == DateTime.Now.Month);
        }

        public async Task<bool> CheckSpam(string ip)
        {
            if (_ctx.CheckSpams.Any(x => x.Ip == ip))
            {
                DateTime check = _ctx.CheckSpams.Last(x => x.Ip == ip).Created;
                double minutes = DateTime.Now.Subtract(check).TotalMinutes;
                if (minutes > 30)
                {
                    await AddToProtection(ip);
                    return true;
                }
                return false;
            }
            else
            {
                await AddToProtection(ip);
                return true;
            }

        }

        private async Task AddToProtection(string ip)
        {
            var temp = new CheckSpam
            {
                Created = DateTime.Now,
                Ip = ip
            };
            _ctx.CheckSpams.Add(temp);
            await _ctx.SaveChangesAsync();
        }
    }
}