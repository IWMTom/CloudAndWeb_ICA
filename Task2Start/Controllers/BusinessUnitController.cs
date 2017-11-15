using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using HebbraCoDbfModel;
using Task2Start.Models;

namespace Task2Start.Controllers
{
    /*  
     *  A Web API Service utilises the ApiController class, rather than the Controller class from Task 1.
     *  The difference is that the ApiController serialises the output into a format that a computer would expect (JSON or XML),
     *  rather than a HTML view. Additionally, the routing engine is different by default; URLs are mapped to actions - an
     *  example of this is the Get() method below - this is called when a HTTP GET request comes in at /BusinessUnit
     */ 

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BusinessUnitController : ApiController
    {

        private HebbraCoDbfModel.HebbraCo16Model context = new HebbraCo16Model(); // A database context reference

        // Called when a HTTP GET request comes in at /BusinessUnit
        public IEnumerable<BusinessUnitDTO> Get()
        {
            var allBu = context.BusinessUnits.Where(b => b.Active == true); // Gets a collection of all Business Units where they are not soft deleted
            var dto = BusinessUnitDTO.buildList(allBu); // Passes the collection to the Business Unit Data Transfer Object to be formatted
            return dto; // Returns the fomatted data (as JSON or XML, depending on which is chosen)
        }
    }
}
