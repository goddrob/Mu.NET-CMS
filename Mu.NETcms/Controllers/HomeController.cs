using Mu.NETcms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mu.NETcms.Logic;

namespace Mu.NETcms.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View(GameCache.GetServerNews());
        }

        public ActionResult ViewPost(int id)
        {
            return View(GameCache.GetPostById(id));
        }
        public ActionResult Download()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}