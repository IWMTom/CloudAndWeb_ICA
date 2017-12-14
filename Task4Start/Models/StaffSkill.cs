using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task4Start.Models
{
    public class StaffSkill
    {
        public string skillCode { get; set; }

        public string staffCode { get; set; }

        public string fullName { get; set; }
    }
}