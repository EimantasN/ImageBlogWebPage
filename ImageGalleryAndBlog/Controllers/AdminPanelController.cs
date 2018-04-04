using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ImageGalleryAndBlog.Data;
using ImageGalleryAndBlog.Models;
using System.Threading.Tasks;

namespace ImageGalleryAndBlog.Controllers
{

    public class AdminPanelController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IImageBlog _imageBlogService;

        public AdminPanelController(IConfiguration config, IImageBlog imageService)
        {
            _imageBlogService = imageService;

            _config = config;
            AzureConnectionString = _config["AzureStorageConnectionString"];
        }

        private string AzureConnectionString { get; }

        [Authorize]
        public IActionResult DataBaseConnect()
        {
            return View();
        }

        [Authorize]
        public IActionResult Forms()
        {
            return View();
        }

        [Authorize]
        public IActionResult Index()
        {
            var model = new IndexModel
            {
                Visitors = _imageBlogService.GetVisitors()
            };
            return View(model);
        }

        [Authorize]
        public IActionResult Tables()
        {
            var model = new TableModel()
            {
                Images = _imageBlogService.GetAllImages(),
                ImageTags = _imageBlogService.GetImageTags(),
                Article = _imageBlogService.GetAllArticle(null, null),
                ArticleTags = _imageBlogService.GetArticleTags(),
                ArticleComment = _imageBlogService.GeArticleComments(),
                Subscribers = _imageBlogService.GetSubscriberses(),
                ArticleReferences = null
            };
            return View(model);
        }

        [Authorize]
        public IActionResult ImageGalleryInsert()
        {
            return View();
        }

        [Authorize]
        public IActionResult ImageGalleryDelete()
        {
            return View();
        }

        [Authorize]
        public IActionResult ImageGalleryUpdate()
        {
            return View();
        }

        [Authorize]
        public IActionResult ArticleInsert()
        {
            return View();
        }

        [Authorize]
        public IActionResult ArticleDelete()
        {
            return View();
        }

        [Authorize]
        public IActionResult ArticleUpdate()
        {
            return View();
        }

        [Authorize]
        public IActionResult ArticleReferenceInsert()
        {
            return View();
        }

        [Authorize]
        public IActionResult ArticleReferenceDelete()
        {
            return View();
        }

        [Authorize]
        public IActionResult ArticleReferenceUpdate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ImageInsert(string category, string tags, string url)
        {
            await _imageBlogService.ImageInsert(category, tags, url);
            return RedirectToAction("DataBaseConnect", "AdminPanel");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ImageDelete(string DeleteUrl)
        {
            await _imageBlogService.DeleteImageByUrl(DeleteUrl);
            return RedirectToAction("DataBaseConnect", "AdminPanel");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ImageUpdateByUlr(string OldUrl, string category, string tags)
        {
            await _imageBlogService.ImageUpdateByUlr(OldUrl, category, tags);
            return RedirectToAction("DataBaseConnect", "AdminPanel");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ArticleInsert(string Title, string Author, string SubText, string Text, string ImgUrl,
            string Category, string Tags)
        {
            await _imageBlogService.ArticleInsert(Title, Author, SubText, Text, ImgUrl, Category, Tags);
            return RedirectToAction("DataBaseConnect", "AdminPanel");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ArticleDelete(string Title)
        {
            await _imageBlogService.ArticleDelete(Title);
            return RedirectToAction("DataBaseConnect", "AdminPanel");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ArticleUpdate(string OldTitle, string Title, string Author, string SubText, string Text,
            string ImgUrl, string Category, string Tags)
        {
            await _imageBlogService.ArticleUpdate(OldTitle, Title, Author, SubText, Text, ImgUrl, Category, Tags);
            return RedirectToAction("DataBaseConnect", "AdminPanel");
        }

        //[HttpPost]
        //public void UploadNewImage(string category, string tags, string url)
        //{
        //    _imageService.ImageInsert(category, tags, url);

        //    RedirectToAction("index", "Gallery");
        //}

        //public IActionResult Delete()
        //{
        //    var model = new DeleteImageModel();
        //    return View(model);
        //}

        //[HttpPost]
        //public void DeleteImage(string DeleteUrl)
        //{
        //    _imageService.DeleteImageByUrl(DeleteUrl);

        //    RedirectToAction("index", "Gallery");
        //}

        //[HttpPost]
        //public async Task<IActionResult> UploadNewImage(IFormFile file, string title, string tags)
        //{
        //    var container = _imageService.GetBlobContainer(AzureConnectionString, "images");

        //    var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
        //    var FileName = content.FileName.Trim('"');

        //    var blockBlob = container.GetBlockBlobReference(FileName);
        //    await blockBlob.UploadFromStreamAsync(file.OpenReadStream());
        //    await _imageService.ImageInsert(title, tags, blockBlob.Uri);

        //    return RedirectToAction("index", "Gallery");
        //}
    }
}