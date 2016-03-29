using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMaze.DataAccessLayer;

namespace QMaze.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            //TO DO
            string message = "Your high score : ";
            
            using (QuestionContext db = new QuestionContext())
            {
                foreach (var q in db.Questions)
                    message += q.Text;
            }
            
            ViewBag.Message = message;

            return View();
        }
    }
}