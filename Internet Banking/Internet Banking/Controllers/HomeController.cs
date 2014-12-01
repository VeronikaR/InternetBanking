using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Internet_Banking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Доброе утро" : (hour < 18 ? "Добрый день" : "Добрый вечер");
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            //ViewBag.Message = "О нас";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
    }
}
