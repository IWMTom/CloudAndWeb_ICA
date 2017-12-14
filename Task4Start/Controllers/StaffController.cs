using HebbraCoDbfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Task4Start.Controllers
{
    public class StaffController : Controller
    {
        StaffSkillsDbfModel.StaffSkillsDbfModel skill_db = new StaffSkillsDbfModel.StaffSkillsDbfModel();
        SkillService.SkillServiceClient WCFClient = new SkillService.SkillServiceClient();

        // GET: Staff/buCode
        public ActionResult Index(string id)
        {
            HttpClient sClient = new HttpClient();
            sClient.BaseAddress = new System.Uri("http://localhost:65026");
            sClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = sClient.GetAsync("api/Staff/BusinessUnit/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var staff = response.Content.ReadAsAsync<IEnumerable<Models.StaffDTO>>().Result;
                return View(staff);
            }
            return View();
        }

        public ActionResult Skills(string id)
        {
            Models.StaffDTO staff = null;

            HttpClient sClient = new HttpClient();
            sClient.BaseAddress = new System.Uri("http://localhost:65026");
            sClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = sClient.GetAsync("api/Staff/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                staff = response.Content.ReadAsAsync<Models.StaffDTO>().Result;
            }

            var thisStaffSkills = skill_db.staffSkills.Where(s => s.staffCode == id).ToList();

            var skillVM = thisStaffSkills.Select(c => new Models.SkillVM
            {
                skillCode = c.skillCode,
                skillDescription = WCFClient.GetSkill(c.skillCode).skillDescription,
                staffCode = c.staffCode
            }).ToList();

            var vm = new Models.SkillAndStaffVM
            {
                skills = skillVM,
                staffMember = staff
            };

            return View(vm);
        }

        public ActionResult AddSkill(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new HttpException(400, "Bad Request");
            }

            Models.StaffDTO staff = null;

            HttpClient sClient = new HttpClient();
            sClient.BaseAddress = new System.Uri("http://localhost:65026");
            sClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = sClient.GetAsync("api/Staff/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                staff = response.Content.ReadAsAsync<Models.StaffDTO>().Result;
            }

            var skills = WCFClient.GetAllSkills();
            var allSkillsCode = (from s in skills select s.skillCode).ToList();
            var thisStaffSkillsCode = (from s in skill_db.staffSkills
                                       where s.staffCode == id
                                       select s.skillCode).ToList();
            var yetToHave = allSkillsCode.Except(thisStaffSkillsCode);

            List<Models.SkillVM> list = Models.SkillVM.buildList(skills, yetToHave);
            ViewBag.skillCode = new SelectList(list, "skillCode", "skillDescription");

            Models.StaffSkill staffSkillVM = new Models.StaffSkill
            {
                staffCode = staff.staffCode,
                fullName = staff.fullName
            };

            return View(staffSkillVM);
        }

        [HttpPost]
        public ActionResult AddSkill(Models.StaffSkill model)
        {
            StaffSkillsDbfModel.staffSkill newSkill = new StaffSkillsDbfModel.staffSkill
            {
                skillCode = model.skillCode,
                staffCode = model.staffCode,
                active = true
            };

            skill_db.staffSkills.Add(newSkill);
            skill_db.SaveChanges();

            return RedirectToAction("Skills", new { id = model.staffCode });

        }
    }
}