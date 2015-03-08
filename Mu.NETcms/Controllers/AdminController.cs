using Mu.NETcms.Logic;
using Mu.NETcms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mu.NETcms.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsList()
        {
            using (var context = new WebDbContext())
            {
                return View(context.News.ToList());
            }
        }
        public ActionResult NewsCreate()
        {
            return View(new NewsViewModel());
        }
        [HttpPost]
        public ActionResult NewsCreate(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                NewsPost npost = new NewsPost();
                npost.Date = DateTime.Now;
                npost.Title = model.Title;
                npost.Image = model.ImageFile;
                npost.Shares = 0;
                npost.HtmlContent = model.HtmlContent;
                npost.Author = "Admin";
                using (var c = new WebDbContext())
                {
                    c.News.Add(npost);
                    c.SaveChanges();
                }
                GameCache.ReCache(false, false, true);
            }
            
            return RedirectToAction("NewsList","Admin");
        }
        public ActionResult NewsEdit(int id) 
        {
            using (var c = new WebDbContext())
            {
                var post = c.News.Find(id);
                if (post == null) return new HttpNotFoundResult();
                NewsViewModel mdl = new NewsViewModel();
                mdl.Id = post.Id;
                mdl.ImageFile = post.Image;
                mdl.Title = post.Title;
                mdl.HtmlContent = post.HtmlContent;
                return View(mdl);
            } 
        }
        [HttpPost]
        public ActionResult NewsEdit(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var c = new WebDbContext())
                {
                    var oPost = c.News.Find(model.Id);
                    oPost.HtmlContent = model.HtmlContent;
                    oPost.Image = model.ImageFile;
                    oPost.Title = model.Title;
                    oPost.Date = DateTime.Now;
                    c.SaveChanges();
                }
                GameCache.ReCache(false, false, true);
                return RedirectToAction("NewsList");
            }
            return View(model);
        }

        public ActionResult NewsDelete(int id)
        {
            using (var c = new WebDbContext())
            {
                c.News.Remove(c.News.Find(id));
                c.SaveChanges();
            }
            GameCache.ReCache(false, false, true);
            return RedirectToAction("NewsList");
        }


        public ActionResult UploadImage()
        {
            return View(new ImageViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(ImageViewModel model)
        {
            var validImageTypes = new string[]{"image/gif","image/jpeg","image/pjpeg","image/png"};
            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose a valid image file.");
            }
            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var baseDir = "~/Content/Images/" + model.category;
                    var imagePath = Path.Combine(Server.MapPath(baseDir), model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);
                }
                
                return RedirectToAction("Index");
            }


            return View(model);
        }
        
    }
}