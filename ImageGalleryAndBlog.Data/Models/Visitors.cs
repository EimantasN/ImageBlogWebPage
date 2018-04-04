using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGalleryAndBlog.Data.Models
{
    public class Visitors
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public DateTime Created { get; set; }
    }
}
