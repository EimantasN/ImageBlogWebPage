using System;
using System.Collections.Generic;

namespace ImageGalleryAndBlog.Data.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; }
        public string Url { get; set; }

        public virtual IEnumerable<ImageTags> Tags { get; set; }
    }
}