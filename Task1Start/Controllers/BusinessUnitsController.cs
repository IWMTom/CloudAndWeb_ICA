using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HebbraCoDbfModel;
using Task1Start.Models;

namespace Task1Start.Controllers
{
    public class BusinessUnitsController : Controller
    {
        private HebbraCo16Model db = new HebbraCo16Model(); // An instance of the database context

        // GET: BusinessUnits
        public ActionResult Index()
        {
            var allBu = db.BusinessUnits.Where(b => b.Active == true); // Gets all business units from the database where the active flag is true as a collection of raw data
            var viewModel = Models.BusinessUnitVM.buildList(allBu); // Passes the collection of business units to the view model to get a collection of formatted data
            return View(viewModel); // Passes the collection of formatted data to the Razor view engine to render to the screen, using /Views/BusinessUnits/Index.cshtml
        }

        // GET: BusinessUnits/Details/5
        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // If an ID isn't provided in the URL, a HTTP 400 code is thrown
            }

            var thisBu = db.BusinessUnits.SingleOrDefault(bu => bu.businessUnitCode.Equals(id, StringComparison.OrdinalIgnoreCase)); // Gets the business unit where the code equals the ID from the URL, regardless of case - equals null if not found
            if (thisBu.Active == false || thisBu == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // If the business unit is soft-deleted, or doesn't exist for the given ID, a HTTP 400 code is thrown
            }
            else
            {

                var viewModel = BusinessUnitDetailVM.buildViewModel(thisBu); // Passes the business unit to the view model to get an object with formatted data
                return View(viewModel); // Passes the formatted business unit to the Razor view engine to render to the screen, using /Views/BusinessUnits/Details.cshtml
            }



          
            
        }

        // GET: BusinessUnits/Create
        public ActionResult Create()
        {
            return View(); // Tells the Razor view engine to render /Views/BusinessUnits/Create.cshtml
        }

        // POST: BusinessUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // Tells ASP.NET MVC that we don't want to be vulnerable to CSRF attacks!
        public ActionResult Create([Bind(Include = "businessUnitCode,title,description,officeAddress1,officeAddresss2,officeAddress3,officePostCode,officeContact,officePhone,officeEmail")] Task1Start.Models.BusinessUnitDetailVM businessUnitVM)
        {
            if (ModelState.IsValid) // If validation checks pass...
            {
                var model = BusinessUnitDetailVM.buildModel(businessUnitVM); // Passes the view model data and gets back a BusinessUnit model
                model.Active = true; // Sets the active flag to true (it's not been soft deleted!)
                db.BusinessUnits.Add(model); // Inserts the data to the database as a new row
                db.SaveChanges(); // Saves the changes to the database
                return RedirectToAction("Index"); // Redirects to the BusinessUnits list
            }

            return View(businessUnitVM); // Returns back to the creation form with the errors from validation
        }

        // GET: BusinessUnits/Edit/5
        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // If an ID isn't provided in the URL, a HTTP 400 code is thrown
            }

            var thisBu = db.BusinessUnits.SingleOrDefault(bu => bu.businessUnitCode.Equals(id, StringComparison.OrdinalIgnoreCase)); // Gets the business unit where the code equals the ID from the URL, regardless of case - equals null if not found

            if (thisBu.Active == false || thisBu == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // If the business unit is soft-deleted, or doesn't exist for the given ID, a HTTP 400 code is thrown
            }
            else
            {

                var viewModel = BusinessUnitDetailVM.buildViewModel(thisBu); // Passes the business unit to the view model to get an object with formatted data
                return View(viewModel); // Passes the formatted business unit to the Razor view engine to render to the screen, using /Views/BusinessUnits/Edit.cshtml
            }
        }

        // POST: BusinessUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // Tells ASP.NET MVC that we don't want to be vulnerable to CSRF attacks!
        public ActionResult Edit([Bind(Include = "businessUnitCode,title,description,officeAddress1,officeAddresss2,officeAddress3,officePostCode,officeContact,officePhone,officeEmail")] BusinessUnitDetailVM businessUnitVM)
        {
            if (ModelState.IsValid) // If validation checks pass...
            {
                var efmodel = db.BusinessUnits.SingleOrDefault(bu => bu.businessUnitCode.Equals(businessUnitVM.businessUnitCode, StringComparison.OrdinalIgnoreCase)); // Gets the business unit where the code equals the ID from the URL, regardless of case - equals null if not found
                var model = BusinessUnitDetailVM.buildModel(businessUnitVM, efmodel); // Turns the view model into an edited version of the raw data model
                db.Entry(model).State = EntityState.Modified; // Tells the database context that the model is being updated
                db.SaveChanges(); // Saves changes to the database
                return RedirectToAction("Index"); // Redirects to the listing of BusinessUnits
            }
            return View(businessUnitVM); // Returns back to the edit form with the errors from validation
        }

        // GET: BusinessUnits/Delete/5
        public ActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // If an ID isn't provided in the URL, a HTTP 400 code is thrown
            }

            var thisBu = db.BusinessUnits.SingleOrDefault(bu => bu.businessUnitCode.Equals(id, StringComparison.OrdinalIgnoreCase)); // Gets the business unit where the code equals the ID from the URL, regardless of case - equals null if not found
            if (thisBu.Active == false || thisBu == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // If the business unit is soft-deleted, or doesn't exist for the given ID, a HTTP 400 code is thrown
            }
            else
            {

                var viewModel = BusinessUnitDetailVM.buildViewModel(thisBu); // Passes the business unit to the view model to get an object with formatted data
                return View(viewModel); // Passes the formatted business unit to the Razor view engine to render to the screen, using /Views/BusinessUnits/Delete.cshtml
            }
        }

        // POST: BusinessUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Tells ASP.NET MVC that we don't want to be vulnerable to CSRF attacks!
        public ActionResult DeleteConfirmed(string id)
        {
            var thisBu = db.BusinessUnits.SingleOrDefault(bu => bu.businessUnitCode.Equals(id, StringComparison.OrdinalIgnoreCase)); // Gets the business unit where the code equals the ID from the URL, regardless of case - equals null if not found
            thisBu.Active = false; // Sets the soft-delete flag to false (it'll act as if it's deleted)
            db.Entry(thisBu).State = EntityState.Modified; // Tells the database context that the model is being updated
            db.SaveChanges(); // Saves changes to the database
            return RedirectToAction("Index"); // Redirects to the listing of BusinessUnits
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
