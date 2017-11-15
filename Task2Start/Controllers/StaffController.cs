using HebbraCoDbfModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Task2Start.Models;

namespace Task2Start.Controllers
{
    public class StaffController : ApiController
    {
        private HebbraCoDbfModel.HebbraCo16Model context = new HebbraCo16Model(); // A database context reference

        // Called when a HTTP GET request comes in at /api/Staff/{staffCode}
        [Route("api/Staff/{staffCode}")]
        public StaffDetailDTO Get(string staffCode)
        {
            if (String.IsNullOrEmpty(staffCode))
            {
                throw new HttpException(400, "Bad Request"); // If an ID isn't provided in the URL, a HTTP 400 exception is thrown
            }

            var thisStaff = context.Staffs.FirstOrDefault(s => s.staffCode.Equals(staffCode, StringComparison.OrdinalIgnoreCase) && s.Active == true); // Gets the staff member where the code equals the ID from the URL, regardless of case, and isn't soft deleted - equals null if not found
            if (thisStaff == null)
            {
                throw new HttpException(404, "Not Found"); // If the staff member doesn't exist for the given ID, a HTTP 404 exception is thrown
            }
            else
            {
                var dto = StaffDetailDTO.buildDTO(thisStaff); // Passes the collection to the Staff Data Transfer Object to be formatted
                return dto;
            }
        }

        // Called when a HTTP GET request comes in at /api/Staff//BusinessUnit/{buCode}
        [Route("api/Staff/BusinessUnit/{buCode}")]
        public IEnumerable<StaffDetailDTO> getStaffByBusinessUnit(string buCode)
        {
            if (String.IsNullOrEmpty(buCode))
            {
                throw new HttpException(400, "Bad Request"); // If an ID isn't provided in the URL, a HTTP 400 exception is thrown
            }

            var staff = context.Staffs.Where(s => s.Active == true && s.BusinessUnit.businessUnitCode.Equals(buCode, StringComparison.OrdinalIgnoreCase)); // Gets all staff from the database where the active flag is true and the business unit code matches as a collection of raw data
            var dto = StaffDetailDTO.buildList(staff); // Passes the collection to the Staff Detail Data Transfer Object to be formatted
            return dto; // Returns the fomatted data (as JSON or XML, depending on which is chosen)
        }
    }
}
