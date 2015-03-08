using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mu.NETcms.Models
{
    
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Image")]
        public string ImageFile { get; set; }
        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Page Content")]
        public string HtmlContent { get; set; }

    }
    public class ImageViewModel
    {
        [Required]
        [Display(Name = "File Name")]
        public string fname { get; set; }
        [Required]
        [Display(Name = "Category(Subdir)")]
        public string category { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Image Path")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}