using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMaze.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace QMaze.Controllers
{
    public class GameController : Controller
    {
        public ApplicationUser AppUser
        {
            get
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                return manager.FindByName(User.Identity.Name);
            }
        }


        // GET: Game
        public ActionResult Index()
        {
            SetupGameModel model = new SetupGameModel();

            

            //ViewBag.Level = levels;

            return View(model);
        }

        [HttpGet]
        public ActionResult Start(string playerID, int level)
        {
            if (!User.Identity.IsAuthenticated)
                return Error("You have to be logged in to play");

            if (playerID == null || playerID == "")
            {
                playerID = AppUser.Id;
            }

            if (level < 0)
                level = 1;

            GameModel model = new GameModel(playerID, level);
            return View(model);
        }


        [HttpPost]
        public void LevelSelected(SetupGameModel model)
        {
            ViewBag.SelectedLevel = 6;
            //return View();
        }

        public ActionResult Error(string Message)
        {
            ViewBag.Message = Message;
            return View("~/Views/Shared/Error.cshtml");
        }

    }
}