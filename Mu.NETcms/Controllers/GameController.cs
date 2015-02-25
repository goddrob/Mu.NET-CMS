using Mu.NETcms.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Mu.NETcms.Models;
using System.Threading.Tasks;
using System.Text;

namespace Mu.NETcms.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private ApplicationUserManager _userManager;
        public GameController()
        {

        }
        public GameController(ApplicationUserManager userManager){
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        // GET: Game
        public async Task<ActionResult> Index(GameMessageId? messageId)
        {
            ViewBag.StatusMessage =
                messageId == GameMessageId.ResetSuccess ? "Character successfully reseted."
                : messageId == GameMessageId.AccountConnected ? "You need to log out from the game first."
                : messageId == GameMessageId.ResetFailLevel ? "No reset level."
                : messageId == GameMessageId.ResetFailZen ? "Not enough zen."
                : messageId == GameMessageId.ResetFailCap ? "Reached maximum resets."
                : messageId == GameMessageId.Error ? "An error has occured."
                : "";

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            ViewBag.Chars = GameManager.Create().GetCharsFor(user.GameId);
            byte[] vault = GameManager.Create().GetVaultFor(user.GameId).Items;
            ViewBag.Vault = ItemManager.VaultToList(vault);
            return View();
        }
        
        //
        //POST: /Game/Reset
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reset(string name)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());            
            GameMessageId response = GameManager.Create().ResetCharacter(user.GameId, name);
            return RedirectToAction("Index", new { messageId = response});
        }
    }
}