using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Task4Start.Controllers
{
    public class BusinessUnitController : Controller
    {
        StaffSkillsDbfModel.StaffSkillsDbfModel skill_db = new StaffSkillsDbfModel.StaffSkillsDbfModel();
        SkillService.SkillServiceClient WCFClient = new SkillService.SkillServiceClient();

        // GET: BusinessUnit
        public ActionResult Index()
        {
            HttpClient buClient = new HttpClient();
            buClient.BaseAddress = new System.Uri("http://localhost:65026");
            buClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = buClient.GetAsync("api/BusinessUnit").Result;
            if (response.IsSuccessStatusCode)
            {
                var bu = response.Content.ReadAsAsync<IEnumerable<Models.BusinessUnitDTO>>().Result;
                return View(bu);
            }

            return View();
        }

        public ActionResult Skills(string id)
        {
            IEnumerable<Models.StaffDTO> staffList = null;
            List<Models.SkillVM> skillsList = new List<Models.SkillVM>();

            HttpClient buClient = new HttpClient();
            buClient.BaseAddress = new System.Uri("http://localhost:65026");
            buClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = buClient.GetAsync("api/Staff/BusinessUnit/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                staffList = response.Content.ReadAsAsync<IEnumerable<Models.StaffDTO>>().Result;
            }

            foreach (Models.StaffDTO staffMember in staffList)
            {
                var thisStaffSkills = skill_db.staffSkills.Where(s => s.staffCode == staffMember.staffCode).ToList();

                var skillVM = thisStaffSkills.Select(c => new Models.SkillVM
                {
                    skillCode = c.skillCode,
                    skillDescription = WCFClient.GetSkill(c.skillCode).skillDescription
                }).ToList();

                foreach (Models.SkillVM skill in skillVM)
                {
                    if (!skillsList.Contains(skill))
                        skillsList.Add(skill);
                }
            }

            return View(skillsList);
        }
    }
}