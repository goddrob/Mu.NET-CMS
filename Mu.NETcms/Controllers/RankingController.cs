using Mu.NETcms.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Mu.NETcms.Controllers
{
    public class RankingController : Controller
    {
        // GET: Ranking
        [AllowAnonymous]
        public ActionResult Index(string type)
        {
            if (type == null || !RankTypes.ContainsKey(type))
                return RedirectToAction("Index", new { type = "all"});
            else return View(GameCache.GetRankChar(RankTypes[type]));
        }
        [AllowAnonymous]
        public ActionResult Guilds()
        {
            return View(GameCache.GetRankGuild());
        }
        public RankingController()
        {
            RankTypes = new Dictionary<string, RankType>();
            RankTypes.Add("all", RankType.ALL_CHAR);
            RankTypes.Add("bk", RankType.BK_CHAR);
            RankTypes.Add("sm", RankType.SM_CHAR);
            RankTypes.Add("elf", RankType.ELF_CHAR);
            RankTypes.Add("sum", RankType.SUM_CHAR);
            RankTypes.Add("mg", RankType.MG_CHAR);
            RankTypes.Add("dl", RankType.DL_CHAR);
            RankTypes.Add("rf", RankType.RF_CHAR);
        }
        private Dictionary<string, RankType> RankTypes { get; set; }
    }
}