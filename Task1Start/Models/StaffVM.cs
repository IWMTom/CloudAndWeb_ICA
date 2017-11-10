using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task1Start.Models
{
    public class StaffVM
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Staff Code")]
        public string staffCode { get; set; }

        [Display(Name = "Full Name")]
        public string fullName { get; set; }

        [Display(Name = "Business Unit Code")]
        public string businessUnitCode { get; set; }

        public static IEnumerable<Models.StaffVM> buildList(IEnumerable<HebbraCoDbfModel.Staff> allStaff)
        {
            var staff = allStaff.Select(s =>
                        new Models.StaffVM()
                        {
                            staffCode = s.staffCode.Trim(),
                            fullName = s.firstName + " " + (s.middleName == null ? "" : (s.middleName[0] + " ")) + s.lastName,
                            businessUnitCode = s.BusinessUnit.businessUnitCode
                        }).AsEnumerable();

            return staff;
        }
    }
}