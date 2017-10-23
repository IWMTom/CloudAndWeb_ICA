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
    public class StaffsController : Controller
    {
        private HebbraCo16Model db = new HebbraCo16Model();

        // GET: Staffs/SCM
        public ActionResult Index(string id)
        {
            var staff = db.Staffs.Where(s => s.Active == true); // Gets all staff from the database where the active flag is true as a collection of raw data

            if (id != null)
            {
                staff = staff.Where(s => s.BusinessUnit.businessUnitCode.Equals(id, StringComparison.OrdinalIgnoreCase)); // Gets all staff where the business unit code matches as a collection of raw data
            }
            
            var viewModel = Models.StaffVM.buildList(staff); // Passes the collection of business units to the view model to get a collection of formatted data
            return View(viewModel); // Passes the collection of formatted data to the Razor view engine to render to the screen, using /Views/Staffs/Index.cshtml
        }

        // GET: Staffs/Details/5
        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new HttpException(400, "Bad Request"); // If an ID isn't provided in the URL, a HTTP 400 exception is thrown
            }

            var thisStaff = db.Staffs.FirstOrDefault(s => s.staffCode.Equals(id, StringComparison.OrdinalIgnoreCase) && s.Active == true); // Gets the staff member where the code equals the ID from the URL, regardless of case, and isn't soft deleted - equals null if not found
            if (thisStaff == null)
            {
                throw new HttpException(404, "Not Found"); // If the staff member doesn't exist for the given ID, a HTTP 404 exception is thrown
            }
            else
            {
                var viewModel = StaffDetailVM.buildViewModel(thisStaff); // Passes the staff member to the view model to get an object with formatted data
                return View(viewModel); // Passes the formatted business unit to the Razor view engine to render to the screen, using /Views/Staffs/Details.cshtml
            }
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.businessUnitId = new SelectList(db.BusinessUnits.Where(b => b.Active == true), "businessUnitId", "businessUnitCode"); // Returns a list of business codes that are active
            return View(); // Tells the Razor view engine to render /Views/Staffs/Create.cshtml
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "businessUnitId,staffCode,firstName,middleName,lastName,dob,startDate,profile,emailAddress")] Task1Start.Models.StaffDetailVM staffVM)
        {
            if (ModelState.IsValid) // If validation checks pass...
            {
                var model = StaffDetailVM.buildModel(staffVM); // Passes the view model data and gets back a BusinessUnit model
                model.Active = true; // Sets the active flag to true (it's not been soft deleted!)
                db.Staffs.Add(model); // Inserts the data to the database as a new row
                db.SaveChanges(); // Saves the changes to the database
                return RedirectToAction("Index"); // Redirects to the BusinessUnits list
            }

            
            ViewBag.businessUnitId = new SelectList(db.BusinessUnits.Where(b => b.Active == true), "businessUnitId", "businessUnitCode"); // Returns a list of business codes that are active
            return View(staffVM); // Returns back to the creation form with the errors from validation
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new HttpException(400, "Bad Request"); // If an ID isn't provided in the URL, a HTTP 400 exception is thrown
            }

            var thisStaff = db.Staffs.FirstOrDefault(s => s.staffCode.Equals(id, StringComparison.OrdinalIgnoreCase) && s.Active == true); // Gets the staff member where the code equals the ID from the URL, regardless of case, and isn't soft deleted - equals null if not found
            if (thisStaff == null)
            {
                throw new HttpException(404, "Not Found"); // If the staff member doesn't exist for the given ID, a HTTP 404 exception is thrown
            }
            else
            {
                ViewBag.businessUnitId = new SelectList(db.BusinessUnits.Where(b => b.Active == true), "businessUnitId", "businessUnitCode", thisStaff.businessUnitId); // Returns a list of business codes that are active

                var viewModel = StaffDetailVM.buildViewModel(thisStaff); // Passes the staff member to the view model to get an object with formatted data
                return View(viewModel); // Passes the formatted staff member to the Razor view engine to render to the screen, using /Views/Staffs/Edit.cshtml
            }
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, [Bind(Include = "businessUnitId,staffCode,firstName,middleName,lastName,dob,startDate,profile,emailAddress")] StaffDetailVM staffVM)
        {
            if (ModelState.IsValid)
            {
                var efmodel = db.Staffs.FirstOrDefault(s => s.staffCode.Equals(staffVM.staffCode, StringComparison.OrdinalIgnoreCase) && s.Active == true); // Gets the business unit where the code equals the ID from the URL, regardless of case - equals null if not found
                var model = StaffDetailVM.buildModel(staffVM, efmodel); // Turns the view model into an edited version of the raw data model
                db.Entry(model).State = EntityState.Modified; // Tells the database context that the model is being updated
                db.SaveChanges(); // Saves changes to the database
                return RedirectToAction("Index"); // Redirects to the listing of BusinessUnits
            }

            ViewBag.businessUnitId = new SelectList(db.BusinessUnits.Where(b => b.Active == true), "businessUnitId", "businessUnitCode", staffVM.businessUnitId); // Returns a list of business codes that are active
            return View(staffVM);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new HttpException(400, "Bad Request"); // If an ID isn't provided in the URL, a HTTP 400 exception is thrown
            }

            var thisStaff = db.Staffs.FirstOrDefault(s => s.staffCode.Equals(id, StringComparison.OrdinalIgnoreCase) && s.Active == true); // Gets the staff member where the code equals the ID from the URL, regardless of case, and isn't soft deleted - equals null if not found
            if (thisStaff == null)
            {
                throw new HttpException(404, "Not Found"); // If the business unit doesn't exist for the given ID, a HTTP 404 exception is thrown
            }
            else
            {
                var viewModel = StaffDetailVM.buildViewModel(thisStaff); // Passes the staff member to the view model to get an object with formatted data
                return View(viewModel); // Passes the formatted staff member to the Razor view engine to render to the screen, using /Views/Staffs/Delete.cshtml
            }
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var thisStaff = db.Staffs.FirstOrDefault(s => s.staffCode.Equals(id, StringComparison.OrdinalIgnoreCase) && s.Active == true); // Gets the staff member where the code equals the ID from the URL, regardless of case - equals null if not found

            thisStaff.Active = false; // Sets the soft-delete flag to false (it'll act as if it's deleted)
            db.Entry(thisStaff).State = EntityState.Modified; // Tells the database context that the model is being updated
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
