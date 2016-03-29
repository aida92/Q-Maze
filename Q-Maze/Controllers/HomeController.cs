using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMaze.DataAccessLayer;
using QMaze.Models;

namespace QMaze.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statistics(string Id)
        {
            if (Id == null || Id == "")
            {
                Id = "2fbb8918-4128-456c-9cab-4f0521e575ca";//TO DO
            }
            UserStatisticsModel model = new UserStatisticsModel(Id);
            return View(model);
        }
    }
}