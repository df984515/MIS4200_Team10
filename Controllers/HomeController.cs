using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS4200_Team10.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Culture Application";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "To Contact Us:";

            return View();
        }
    }
}