using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HebbraCoDbfModel;

namespace Task2Start.Models
{
    /*
     * The Data Transfer Object shapes data similarly to a view model from Task 1.
     */

    public class BusinessUnitDTO
    {
        public string businessUnitCode { get; set; }

        public string title { get; set; }

        public static IEnumerable<BusinessUnitDTO> buildList(IEnumerable<BusinessUnit> Allbu)
        {
            var busUnits = Allbu.Select(b =>
                       new Models.BusinessUnitDTO()
                       {
                           businessUnitCode = b.businessUnitCode.Trim(),
                           title = b.title.Trim(),
                       }).AsEnumerable();
            return busUnits;
        }
    }
}