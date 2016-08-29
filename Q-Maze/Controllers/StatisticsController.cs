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
    
    public class StatisticsController : Controller
    {
        public ApplicationUser AppUser
        {
            get
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                return manager.FindByName(User.Identity.Name);
            }
        }
        

        // GET: Statistics
        public ActionResult Index()
        {
            GameStatisticsModel model = new GameStatisticsModel();
            return View(model);
        }

        public ActionResult PlayerStatistics(string Id)
        {
            if (!User.Identity.IsAuthenticated)
                return Error("You have to be logged in to access individual statistics");

            if (Id == null || Id == "")
            {
                Id = AppUser.Id;
            }
            UserStatisticsModel model = new UserStatisticsModel(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult RefreshList(GameStatisticsModel model)
        {
            if (ModelState.IsValid)
            {
                model.RefreshList();
            }
            return PartialView("_PlayerList", model);
        }

        public ActionResult Error(string Message)
        {
            ViewBag.Message = Message;
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}