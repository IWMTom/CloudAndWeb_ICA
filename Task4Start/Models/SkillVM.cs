using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task4Start.Models
{
    public class SkillVM
    {
        [Display(Name = "Skill Code")]
        public string skillCode { get; set; }

        [Display(Name = "Description")]
        public string skillDescription { get; set; }

        public string staffCode { get; set; }

        public static List<Models.SkillVM> buildList(IEnumerable<SkillService.SkillsDTO> skills, IEnumerable<string> yetToHave)
        {
            List<Models.SkillVM> list = new List<SkillVM>();

            foreach (SkillService.SkillsDTO skill in skills)
            {
                if (yetToHave.Contains(skill.skillCode))
                {
                    list.Add(new SkillVM
                    {
                        skillCode = skill.skillCode,
                        skillDescription = skill.skillDescription
                    });
                }
            }

            return list;
        }
    }
}