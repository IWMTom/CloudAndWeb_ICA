using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task1Start.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error/NotFound
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: Error/BadRequest
        public ActionResult BadRequest()
        {
            return View();
        }
    }
}