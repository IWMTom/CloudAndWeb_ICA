using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task4Start.Models
{
    public class StaffDTO
    {
        [Display(Name = "Code")]
        public string staffCode { get; set; }

        [Display(Name = "Name")]
        public string fullName { get; set; }
    }
}