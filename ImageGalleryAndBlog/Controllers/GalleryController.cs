using System;
using Microsoft.AspNetCore.Mvc;
using ImageGalleryAndBlog.Data;
using ImageGalleryAndBlog.Data.Models;
using ImageGalleryAndBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace ImageGalleryAndBlog.Controllers
{
    public class GalleryController : Controller
    {

        private IHttpContextAccessor _accessor;

        private readonly IImageBlog _imageService;

        public GalleryController(IImageBlog imageService, IHttpContextAccessor accessor)
        {
            _imageService = imageService;
            _accessor = accessor;
        }

        public async Task<IActionResult> Index(string category, int Id)
        {
            if (category == null)
            {
                await _imageService.AddVisitor(OnGet());
            }
            IEnumerable<GalleryImage> imageList = null;
            try
            {
                if (category == null)
                    imageList = _imageService.GetAllImages();
                else
                    imageList = _imageService.GetImagesByCategory(category);
            }
            catch
            {
                imageList = _imageService.GetAllImages();
            }

            var model = new GalleryIndexModel
            {
                Images = imageList,
                SearchQuery = "",
                Id = Id,
                Kat = _imageService.Kategorijos()
            };
            return View(model);
        }

        public string OnGet()
        {
            return _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public IActionResult Blog(string category, string tag, int recievedPage)
        {
            IEnumerable<Article> arc = null;

            //Needed for paging
            string tempCategory = null;
            string tempTag = null;

            if (category != null)
            {
                tempCategory = category;
                arc = _imageService.GetAllArticle(category, null);
            }
            else if (tag != null)
            {
                tempTag = tag;
                arc = _imageService.GetAllArticle(null, tag);
            }
            else
            {
                arc = _imageService.GetAllArticle(null, null);
            }

            //Need to pass all articles categorys
            if (arc != null)
            {
                var model = new BlogModel
                {
                    Article = arc,
                    Articletags = _imageService.GetDistinctArticleTags(),
                    ArticleCategorys = _imageService.GetDistinctArticleCategorys(),
                    Page = recievedPage,
                    Category = tempCategory,
                    Tag = tempTag,
                    Kat = _imageService.Kategorijos()
                };

                return View(model);
            }

            return View();
        }

        public IActionResult Contact()
        {
            var model = new ContactModel
            {
                Kat = _imageService.Kategorijos()
            };
            return View(model);
        }

        public IActionResult Send(string header, string message)
        {
            var model = new Send
            {
                Kat = _imageService.Kategorijos(),
                Header = header,
                Message = message
            };
            return View(model);
        }

        public IActionResult About()
        {
            var model = new AboutModel
            {
                Images = _imageService.OneImageFormEveryCategory(),
                Kat = _imageService.Kategorijos()
            };
            return View(model);
        }

        public IActionResult FullArticle(int id)
        {
            var model = new ArticleModel
            {
                Article = _imageService.GetArticleById(id)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubscriber(string email)
        {
            await _imageService.AddSubscriber(email);
            return RedirectToAction("Send", "Gallery",
                new { header = "THANK YOU", message = "We will report the new article to you first" });
        }

        [HttpPost]
        public async Task<IActionResult> MakeContact(string name, string lastname, string email, string subject, string Mess)
        {
            if (await _imageService.CheckSpam(OnGet()))
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("mail.smtp2go.com");
                    message.From = new MailAddress(email);
                    message.To.Add("noreika.eimantas@gmail.com");
                    message.Subject = subject + " From - " + name + " " + lastname;
                    message.Body = Mess;
                    smtpServer.Port = 2525;
                    smtpServer.Credentials =
                        new System.Net.NetworkCredential("noreika.eimantas@gmail.com", "NpVeaDOotk4z");
                    smtpServer.EnableSsl = true;
                    smtpServer.Send(message);

                    return RedirectToAction("Send", "Gallery",
                        new {header = "THANKS", message = "I will contact you soon"});
                }
                catch
                {
                    return RedirectToAction("Send", "Gallery",
                        new {header = "Error", message = "Check whether everything typed well"});
                }
            }
            return RedirectToAction("Send", "Gallery",
                new { header = "Sorry", message = "You can send new message after - 30 minutes from your last" });

        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string firstName, string lastName, string email, string message, int id)
        {
            await _imageService.AddComment(firstName, lastName, email, message, id);
            return RedirectToAction("FullArticle", "Gallery", new { id });
        }
    }
}