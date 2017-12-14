using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task4Start.Models
{
    public class SkillAndStaffVM
    {
        public IEnumerable<SkillVM> skills { get; set; }
        public StaffDTO staffMember { get; set; }
    }
}