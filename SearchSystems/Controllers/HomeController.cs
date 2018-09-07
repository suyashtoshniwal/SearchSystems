using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SearchSystems.Models;

namespace SearchSystems.Controllers
{
    public class HomeController : Controller
    {
        private SEARCHSYSTEMSEntities4 db = new SEARCHSYSTEMSEntities4();

        [Authorize(Roles = "admin")]
        public ActionResult Index(string search, string previousSortOrder,
                                  string previousSortTerm, string CurrentSortTerm,
                                  int? page, bool PFView = false, bool gratuityView = false,
                                  bool insuranceView = false)
        {

            ViewBag.Search = search;
            ViewBag.PFView = PFView;
            previousSortTerm = String.IsNullOrEmpty(previousSortTerm) ? "Name" : previousSortTerm;
            IQueryable<Employee> searchedEmployees = db.Employees;

            if(PFView)
            {
                DateTime testGreaterThanDate = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0));

                searchedEmployees = searchedEmployees.Where(e => e.PFStartDate <= testGreaterThanDate && e.PFStatus == false);

                var searchedEmployees1 = searchedEmployees.ToList();
            }
            if (gratuityView)
            {
                DateTime testGreaterThanDate = DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0));

                searchedEmployees = searchedEmployees.Where(e => e.GratuityStartDate >= testGreaterThanDate && e.GratuityStatus == false);
            }
            if (insuranceView)
            {
                DateTime testGreaterThanDate = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));

                searchedEmployees = searchedEmployees.Where(e => e.InsuranceRenewalDate == null || e.InsuranceExpiryDate >= testGreaterThanDate);
            }
            if (!String.IsNullOrEmpty(search))
            {
                searchedEmployees = searchedEmployees.Where(e => e.FirstName.StartsWith(search) || e.LastName.StartsWith(search));
            }
            IEnumerable<Employee> sortedmployees = null;
            string currentSortOrder = string.Empty;

            switch (CurrentSortTerm)
            {
                case "FirstName":

                    if (previousSortTerm.Equals(CurrentSortTerm))
                    {
                        if (previousSortOrder == "ascending")
                        {
                            sortedmployees = searchedEmployees.OrderByDescending
                                 (m => m.FirstName);
                            currentSortOrder = "descending";
                        }
                        else
                        {
                            sortedmployees = searchedEmployees.OrderBy
                                     (m => m.FirstName);
                            currentSortOrder = "ascending";
                        }
                    }
                    else
                    {
                        sortedmployees = searchedEmployees.OrderBy
                                (m => m.FirstName);
                        currentSortOrder = "ascending";
                    }

                    break;
                case "Id":
                    CurrentSortTerm = "Id";
                    if (previousSortTerm.Equals(CurrentSortTerm))
                    {
                        if (previousSortOrder == "ascending")
                        {
                            sortedmployees = searchedEmployees.OrderByDescending
                                 (m => m.Id);
                            currentSortOrder = "descending";
                        }
                        else
                        {
                            sortedmployees = searchedEmployees.OrderBy
                                     (m => m.Id);
                            currentSortOrder = "ascending";
                        }
                    }
                    else

                    {
                        sortedmployees = searchedEmployees.OrderBy
                                  (m => m.Id);
                        currentSortOrder = "ascending";
                    }

                    break;
                case "LastName":
                    CurrentSortTerm = "LastName";
                    if (previousSortTerm.Equals(CurrentSortTerm))
                    {
                        if (previousSortOrder == "ascending")
                        {
                            sortedmployees = searchedEmployees.OrderByDescending
                                 (m => m.LastName);
                            currentSortOrder = "descending";
                        }
                        else
                        {
                            sortedmployees = searchedEmployees.OrderBy
                                     (m => m.LastName);
                            currentSortOrder = "ascending";
                        }
                    }
                    else
                    {
                        sortedmployees = searchedEmployees.OrderBy
                                  (m => m.LastName);
                        currentSortOrder = "ascending";
                    }

                    break;
                case "Gender":
                    CurrentSortTerm = "Gender";
                    if (previousSortTerm.Equals(CurrentSortTerm))
                    {
                        if (previousSortOrder == "ascending")
                        {
                            sortedmployees = searchedEmployees.OrderByDescending
                                 (m => m.Gender);
                            currentSortOrder = "descending";
                        }
                        else
                        {
                            sortedmployees = searchedEmployees.OrderBy
                                     (m => m.Gender);
                            currentSortOrder = "ascending";
                        }
                    }
                    else
                    {
                        sortedmployees = searchedEmployees.OrderBy
                                  (m => m.DepartmentId);
                        currentSortOrder = "ascending";
                    }

                    break;
                case "DepartmentId":
                    CurrentSortTerm = "DepartmentId";
                    if (previousSortTerm.Equals(CurrentSortTerm))
                    {
                        if (previousSortOrder == "ascending")
                        {
                            sortedmployees = searchedEmployees.OrderByDescending
                                 (m => m.DepartmentId);
                            currentSortOrder = "descending";
                        }
                        else
                        {
                            sortedmployees = searchedEmployees.OrderBy
                                     (m => m.DepartmentId);
                            currentSortOrder = "ascending";
                        }
                    }
                    else
                    {
                        sortedmployees = searchedEmployees.OrderBy
                                  (m => m.DepartmentId);
                        currentSortOrder = "ascending";
                    }

                    break;
                default:
                    currentSortOrder = "ascending";
                    sortedmployees = searchedEmployees.OrderBy
                        (m => m.FirstName);
                    CurrentSortTerm = "FirstName";
                    break;
            }
            ViewBag.PreviousSortOrder = currentSortOrder;
            ViewBag.PreviousSortTerm = CurrentSortTerm;
           
            if(PFView)
            {
                return View("PFView", sortedmployees.ToList());
            }
            if (gratuityView)
            {
                return View("GratuityView", sortedmployees.ToList());
            }
            if (insuranceView)
            {
                return View("InsuranceView", sortedmployees.ToList());
            }
            return View(sortedmployees);
        }
        // GET: Employees
        //public ActionResult Index()
        //{
        //    var employees = db.Employees.Include(e => e.Department);
        //    return View(employees.ToList());
        //}

        // GET: Employees/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,DepartmentId,Gender,DOB,BloodGroup,MobileNumber,LandlineNumber,WhatsAppNumber,EmailAddress,OfficeEmailAddress,City,District,State,Country,PinCode,Salary,Designation,DateOfJoining,ProbationPeriod,PFAccountNumber,PFUANNumber,GratuityNumber,MedicalInsuranceNumber,InsuranceExpiryDate,InsuranceRenewalDate,BankAccountNumber,BankName,BankBranchName,BankIFSCCode,AadharNumber,PANNumber,DrivingLicenseNumber,VehicleNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,DepartmentId,Gender,DOB,BloodGroup,MobileNumber,LandlineNumber,WhatsAppNumber,EmailAddress,OfficeEmailAddress,City,District,State,Country,PinCode,Salary,Designation,DateOfJoining,ProbationPeriod,PFAccountNumber,PFUANNumber,GratuityNumber,MedicalInsuranceNumber,InsuranceExpiryDate,InsuranceRenewalDate,BankAccountNumber,BankName,BankBranchName,BankIFSCCode,AadharNumber,PANNumber,DrivingLicenseNumber,VehicleNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        public ActionResult EnablePF([Bind(Include = "Id,PFStatus")] IList<Employee> employeeList)
        {
            var updatedEmployeeList = new List<Employee>();

            foreach (Employee e in employeeList)
            {
                Employee existedEmployee = db.Employees.Find(e.Id);

                existedEmployee.PFStatus = e.PFStatus;

                updatedEmployeeList.Add(e);
            }
            db.SaveChanges();

            return RedirectToAction("Index", new { pfview = true });

        }

        [HttpPost]
        public ActionResult ExportToExcel()
        {
            var allEmployees = db.Employees.ToList();

            var gv = new GridView();
            gv.DataSource = allEmployees;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
        }

        [HttpPost]
        public ActionResult EnableGratuity([Bind(Include = "Id,GratuityStatus")] IList<Employee> employeeList)
        {
            var updatedEmployeeList = new List<Employee>();

            foreach (Employee e in employeeList)
            {
                Employee existedEmployee = db.Employees.Find(e.Id);

                existedEmployee.GratuityStatus = e.GratuityStatus;

                updatedEmployeeList.Add(e);
            }
            db.SaveChanges();

            return RedirectToAction("Index", new { gratuityview = true });

        }

        [HttpPost]
        public ActionResult RenewInsurance([Bind(Include = "Id,InsuranceRenewalDate")] IList<Employee> employeeList)
        {
            var updatedEmployeeList = new List<Employee>();

            foreach (Employee e in employeeList)
            {
                Employee existedEmployee = db.Employees.Find(e.Id);

                existedEmployee.InsuranceRenewalDate = e.InsuranceRenewalDate;

                updatedEmployeeList.Add(e);
            }
            db.SaveChanges();

            return RedirectToAction("Index", new { insuranceview = true });

        }
        // GET: Employees/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
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