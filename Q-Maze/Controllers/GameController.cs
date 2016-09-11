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
            return View(model);
        }

        [HttpGet]
        public ActionResult Start(string playerID, int level)
        {
            if (level < 0)
                level = 1;
            GameModel model;

            if (!User.Identity.IsAuthenticated)
            {
                model = new GameModel(level);
                return View("StartAnonymousGame", model);
            }

            if (playerID == null || playerID == "")
            {
                playerID = AppUser.Id;
            }
            model = new AuthenticatedGameModel(playerID, level);
            return View("StartAuthenticatedGame", model);
        }


        [HttpPost]
        public void LevelSelected(SetupGameModel model)
        {
            ViewBag.SelectedLevel = 6;
            //return View();
        }

        [HttpPost]
        public ActionResult FinishGame(AuthenticatedGameModel model, int score, bool won, int totalQ, int correctQ)
        {
            if (ModelState.IsValid)
            {
                model.TotalScore = score;
                model.Won = won;
                model.TotalQuestions = totalQ;
                model.CorrectQuestions = correctQ;
                model.SaveScore();
            }
            return RedirectToAction("Home", "Index", "Home");
            //return View("FinishGame", score);
        }


        public ActionResult Error(string Message)
        {
            ViewBag.Message = Message;
            return View("~/Views/Shared/Error.cshtml");
        }

    }
}