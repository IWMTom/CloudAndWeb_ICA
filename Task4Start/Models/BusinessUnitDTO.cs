using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task4Start.Models
{
    public class BusinessUnitDTO
    {
        [Display(Name = "Code")]
        public string businessUnitCode { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }
    }
}