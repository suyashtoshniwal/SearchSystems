using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SearchSystems.Models;

namespace SearchSystems
{
    public class EmployeesController : Controller
    {
        private SEARCHSYSTEMSEntities4 db = new SEARCHSYSTEMSEntities4();

        public ActionResult Index(string search, string sortOrder, string CurrentSort, int? page)
        {
            //var employees = db.Employees.Include(e => e.Department);
            int pageSize = 1;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Page = page;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;
            IQueryable<Employee> searchedEmployees = db.Employees;

            if (!String.IsNullOrEmpty(search))
            {
                searchedEmployees = searchedEmployees.Where(e => e.FirstName.StartsWith(search) || e.LastName.StartsWith(search));
            }
            
            IPagedList <Employee> pagedEmployees = null;
           // if(sortOrder == null) { sortOrder = string.Empty; }
            switch (sortOrder)
            {
                case "FirstName":
                    if (sortOrder.Equals(CurrentSort))
                        pagedEmployees = searchedEmployees.OrderByDescending
                                (m => m.FirstName).ToPagedList(pageIndex, pageSize);
                    else
                        pagedEmployees = searchedEmployees.OrderBy
                                (m => m.FirstName).ToPagedList(pageIndex, pageSize);
                    break;
                case "LastName":
                    if (sortOrder.Equals(CurrentSort))
                        pagedEmployees = searchedEmployees.OrderByDescending
                                (m => m.LastName).ToPagedList(pageIndex, pageSize);
                    else
                        pagedEmployees = searchedEmployees.OrderBy
                                (m => m.LastName).ToPagedList(pageIndex, pageSize);
                    break;
                case "MobileNumber":
                    if (sortOrder.Equals(CurrentSort))
                        pagedEmployees = searchedEmployees.OrderByDescending
                                (m => m.MobileNumber).ToPagedList(pageIndex, pageSize);
                    else
                        pagedEmployees = searchedEmployees.OrderBy
                                (m => m.MobileNumber).ToPagedList(pageIndex, pageSize);
                    break;
                case "Salary":
                    if (sortOrder.Equals(CurrentSort))
                        pagedEmployees = searchedEmployees.OrderByDescending
                                (m => m.Salary).ToPagedList(pageIndex, pageSize);
                    else
                        pagedEmployees = searchedEmployees.OrderBy
                                (m => m.Salary).ToPagedList(pageIndex, pageSize);
                    break;
               default:
                   // pagedEmployees = new PagedList<Employee>(new List<Employee>(), 1, 1);
                    pagedEmployees = searchedEmployees.OrderBy
                        (m => m.FirstName).ToPagedList(pageIndex, pageSize);
                    break;
            }
            return View(pagedEmployees);
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
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,DepartmentId,Gender,DOB,BloodGroup,MobileNumber,LandlineNumber,WhatsAppNumber,EmailAddress,OfficeEmailAddress,City,District,State,Country,PinCode,Salary,Designation,DateOfJoining,ProbationPeriod,PFAccountNumber,PFUANNumber,GratuityNumber,MedicalInsuranceNumber,InsuranceStartDate,BankAccountNumber,BankBranchName,BankIFSCCode,AadharNumber,PANNumber,DrivingLicenseNumber,VehicleNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                try { 
                db.SaveChanges();
            }
                catch(Exception e)
                {

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
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,DepartmentId,Gender,DOB,BloodGroup,MobileNumber,LandlineNumber,WhatsAppNumber,EmailAddress,OfficeEmailAddress,City,District,State,Country,PinCode,Salary,Designation,DateOfJoining,ProbationPeriod,PFAccountNumber,PFUANNumber,GratuityNumber,MedicalInsuranceNumber,InsuranceStartDate,BankAccountNumber,BankBranchName,BankIFSCCode,AadharNumber,PANNumber,DrivingLicenseNumber,VehicleNumber")] Employee employee)
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
