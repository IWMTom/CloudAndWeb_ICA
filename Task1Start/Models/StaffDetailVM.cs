using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task1Start.Models
{
    public class StaffDetailVM : StaffVM
    {

        [Required]
        [Display(Name = "Select Business Unit")]
        public int businessUnitId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Middle Name")]
        public string middleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dob { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Display(Name = "Profile")]
        public string profile { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string emailAddress { get; set; }

        public static Models.StaffDetailVM buildViewModel(HebbraCoDbfModel.Staff thisStaff)
        {
            StaffDetailVM vm = new StaffDetailVM
            {
                businessUnitCode = thisStaff.BusinessUnit.businessUnitCode,
                staffCode = thisStaff.staffCode,
                firstName = thisStaff.firstName,
                middleName = thisStaff.middleName,
                lastName = thisStaff.lastName,
                dob = thisStaff.dob,
                startDate = thisStaff.startDate,
                profile = thisStaff.profile,
                emailAddress = thisStaff.emailAddress
            };
            return vm;
        }

        public static HebbraCoDbfModel.Staff buildModel(StaffDetailVM staff)
        {
            HebbraCoDbfModel.Staff s = new HebbraCoDbfModel.Staff();

            s.businessUnitId = staff.businessUnitId;
            s.staffCode = staff.staffCode;
            s.firstName = staff.firstName;
            s.middleName = staff.middleName;
            s.lastName = staff.lastName;
            s.dob = staff.dob;
            s.startDate = staff.startDate;
            s.profile = staff.profile;
            s.emailAddress = staff.emailAddress;
            s.Active = true;
            return s;
        }

        public static HebbraCoDbfModel.Staff buildModel(StaffDetailVM staff, HebbraCoDbfModel.Staff s)
        {
            s.businessUnitId = staff.businessUnitId;
            s.staffCode = staff.staffCode;
            s.firstName = staff.firstName;
            s.middleName = staff.middleName;
            s.lastName = staff.lastName;
            s.dob = staff.dob;
            s.startDate = staff.startDate;
            s.profile = staff.profile;
            s.emailAddress = staff.emailAddress;
            s.Active = true;
            return s;
        }

    }
}