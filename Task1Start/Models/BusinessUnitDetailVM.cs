using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task1Start.Models
{
    public class BusinessUnitDetailVM : BusinessUnitVM
    {
    


        [Required] // Sets the field to be required during validation
        [Display(Name = "Brief Description")] // Sets the display name of the field
        public string description { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string officeAddress1 { get; set; }

        [Required]
        [Display(Name = "Address Line 2")]
        public string officeAddresss2 { get; set; }

        [Required]
        [Display(Name = "Address Line 3")]
        public string officeAddress3 { get; set; }

        [Required]
        [StringLength(10)] // Sets the maximum number of characters for the field
        [Display(Name = "Post Code")]
        [DataType(DataType.PostalCode)] // Sets the data type for validation
        public string officePostCode { get; set; }

        [Required]
        [Display(Name = "Main Contact Full Name")]

        public string officeContact { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string officePhone { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string officeEmail { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string message { get; set; }


        public static Models.BusinessUnitDetailVM buildViewModel(HebbraCoDbfModel.BusinessUnit thisBu)
        {
            BusinessUnitDetailVM vm = new BusinessUnitDetailVM
            {
                businessUnitCode = thisBu.businessUnitCode.Trim(),
                title = thisBu.title,
                description = thisBu.description,
                officeAddress1 = thisBu.officeAddress1,
                officeAddresss2 = thisBu.officeAddresss2,
                officeAddress3 = thisBu.officeAddress3,
                officeContact = thisBu.officeContact,
                officeEmail = thisBu.officeEmail,
                officePhone = thisBu.officePhone,
                officePostCode = thisBu.officePostCode
            };
            return vm;
        }


        public static HebbraCoDbfModel.BusinessUnit buildModel(BusinessUnitDetailVM businessUnit)
        {
            HebbraCoDbfModel.BusinessUnit Bu = new HebbraCoDbfModel.BusinessUnit();
          
            Bu.businessUnitCode = businessUnit.businessUnitCode;
            Bu.title = businessUnit.title;
            Bu.description = businessUnit.description;
            Bu.officeAddress1 = businessUnit.officeAddress1;
            Bu.officeAddresss2 = businessUnit.officeAddresss2;
            Bu.officeAddress3 = businessUnit.officeAddress3;
            Bu.officeContact = businessUnit.officeContact;
            Bu.officeEmail = businessUnit.officeEmail;
            Bu.officePhone = businessUnit.officePhone;
            Bu.officePostCode = businessUnit.officePostCode;
            Bu.Active = true;
            return Bu;
        }


        public static HebbraCoDbfModel.BusinessUnit buildModel(BusinessUnitDetailVM businessUnit, HebbraCoDbfModel.BusinessUnit Bu)
        {
            Bu.businessUnitCode = businessUnit.businessUnitCode;
            Bu.title = businessUnit.title;
            Bu.description = businessUnit.description;
            Bu.officeAddress1 = businessUnit.officeAddress1;
            Bu.officeAddresss2 = businessUnit.officeAddresss2;
            Bu.officeAddress3 = businessUnit.officeAddress3;
            Bu.officeContact = businessUnit.officeContact;
            Bu.officeEmail = businessUnit.officeEmail;
            Bu.officePhone = businessUnit.officePhone;
            Bu.officePostCode = businessUnit.officePostCode;
            Bu.Active = true;
            return Bu;
        }


    }
}