using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storytime.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Products()
        {
            ViewBag.Title = "Products";

            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        public ActionResult ContactUs()
        {
            ViewBag.Title = "Contact Us";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.Title = "Privacy Policy";

            return View();
        }
    }
}
