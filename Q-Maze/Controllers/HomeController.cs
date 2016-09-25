using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMaze.Models;
using System.Globalization;
using System.Threading;


namespace QMaze.Controllers
{
    //[RequireHttps]
    
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Change(string LanguageAbbreviation)
        {
            if (LanguageAbbreviation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbreviation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbreviation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbreviation;
            Response.Cookies.Add(cookie);

            // stay on the page you were BEFORE clicking to change the language
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }
    }
}