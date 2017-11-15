using HebbraCoDbfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task2Start.Models
{
    public class StaffDetailDTO
    {

        public string firstName { get; set; }

        public string middleName { get; set; }

        public string lastName { get; set; }

        public string dob { get; set; }

        public string startDate { get; set; }

        public string profile { get; set; }

        public string emailAddress { get; set; }

        public string staffCode { get; set; }

        public string fullName { get; set; }

        public static StaffDetailDTO buildDTO(Staff s)
        {
            StaffDetailDTO staffDTO = new Models.StaffDetailDTO()
            {
                staffCode = s.staffCode.Trim(),
                fullName = s.firstName + " " + (s.middleName == null ? "" : (s.middleName + " ")) + s.lastName,
                firstName = s.firstName,
                middleName = s.middleName,
                lastName = s.lastName,
                dob = s.dob.ToString(),
                startDate = s.startDate.ToString(),
                profile = s.profile,
                emailAddress = s.emailAddress,
            };
            return staffDTO;
        }

        public static IEnumerable<StaffDetailDTO> buildList(IEnumerable<Staff> allStaff)
        {
            var staff = allStaff.Select(s =>
                       new Models.StaffDetailDTO()
                       {
                           staffCode = s.staffCode.Trim(),
                           fullName = s.firstName + " " + (s.middleName == null ? "" : (s.middleName + " ")) + s.lastName,
                           firstName = s.firstName,
                           middleName = s.middleName,
                           lastName = s.lastName,
                           dob = s.dob.ToString(),
                           startDate = s.startDate.ToString(),
                           profile = s.profile,
                           emailAddress = s.emailAddress,
                       }).AsEnumerable();
            return staff;
        }

    }
}