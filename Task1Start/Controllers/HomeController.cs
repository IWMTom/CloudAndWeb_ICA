using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task1Start.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(); // Tells the Razor view engine to render /Views/Home/Index.cshtml
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(); // Tells the Razor view engine to render /Views/Home/About.cshtml
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(); // Tells the Razor view engine to render /Views/Home/Contact.cshtml
        }
    }
}