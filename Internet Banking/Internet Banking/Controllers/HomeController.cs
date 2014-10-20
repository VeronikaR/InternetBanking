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
            ViewBag.Message = " ЗАО 'Мир Веб технологий'.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница о нас.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов.";
            return View();
        }
    }
}
