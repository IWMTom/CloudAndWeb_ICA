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

        private string _middleName;
        public string middleName
        {
            get { return this._middleName ?? ""; } // If middle name is null, treat as an empty string instead of null
            set { this._middleName = value; }
        }

        public string lastName { get; set; }

        public string dob { get; set; }

        public string startDate { get; set; }

        private string _profile;
        public string profile {
            get { return this._profile ?? ""; } // If profile is null, treat as an empty string instead of null
            set { this._profile = value; }
        }

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
                dob = s.dob.ToString("dd-MM-yyyy"),
                startDate = s.startDate.ToString("dd-MM-yyyy"),
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
                           dob = s.dob.ToString("dd-MM-yyyy"),
                           startDate = s.startDate.ToString("dd-MM-yyyy"),
                           profile = s.profile,
                           emailAddress = s.emailAddress,
                       }).AsEnumerable();
            return staff;
        }

    }
}