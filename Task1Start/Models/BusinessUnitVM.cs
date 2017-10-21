using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task1Start.Models
{
    public class BusinessUnitVM
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string businessUnitCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Unit Name")]
        public string title { get; set; }

        public static IEnumerable<Models.BusinessUnitVM> buildList(IEnumerable<HebbraCoDbfModel.BusinessUnit> allBu)
        {
            var busUnits = allBu.Select(b =>
                        new Models.BusinessUnitVM()
                        {
                            businessUnitCode = b.businessUnitCode.Trim(),
                            title = b.title,
                        }).AsEnumerable();

            return busUnits;
        }



    }
}