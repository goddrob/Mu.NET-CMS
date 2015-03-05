using Mu.NETcms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mu.NETcms.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Administrator")]
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
            return RedirectToAction("NewsList");
        }



        public void UpdateNews(NewsPost original, NewsViewModel post)
        {
            
        }
        public void AddNews(NewsViewModel post)
        {

        }
    }
}