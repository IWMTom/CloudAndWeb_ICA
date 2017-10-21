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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BusinessUnitController : ApiController
    {

        private HebbraCoDbfModel.HebbraCo16Model context = new HebbraCo16Model();

        public IEnumerable<BusinessUnitDTO> Get()
        {
            var allBu = context.BusinessUnits.Where(b => b.Active == true);
            var dto = BusinessUnitDTO.buildList(allBu);
            return dto;
        }
    }
}
