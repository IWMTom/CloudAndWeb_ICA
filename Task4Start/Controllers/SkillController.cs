using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task4Start.Controllers
{
    public class SkillController : Controller
    {
        private SkillService.SkillServiceClient WCFClient = new SkillService.SkillServiceClient();

        // GET: Skill
        public ActionResult Index()
        {
            var skills = WCFClient.GetAllSkills();
            return View(skills);
        }
    }
}