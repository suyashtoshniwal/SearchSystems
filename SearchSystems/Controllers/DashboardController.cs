using SearchSystems.Models;
using SearchSystems.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SearchSystems.Controllers
{
    public class DashboardController : Controller
    {
        private SEARCHSYSTEMSEntities4 db = new SEARCHSYSTEMSEntities4();

        [Authorize(Roles = "admin")]
        public JsonResult GetEmployeeJoiningData()
        {
            var groups = db.Employees.GroupBy(e => e.DateOfJoining.Value.Year)
                            .Select(e => new
                            {
                                Year = e.Key,
                                NoOfEmployees = e.Count()
                            }
                            )
                            .OrderBy(n => n.Year);
            var groupsArray = groups.ToArray();
            var chartData = new object[groups.Count() + 1];
            chartData[0] = new object[] { "Year", "EmployeesJoined" };
            int j = 0;
            foreach (var i in groups)
            {
                j++;
                chartData[j] = new object[] { i.Year, i.NoOfEmployees };
            }
            return new JsonResult { Data = chartData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            var employeeDashboardViewModel = new EmployeeDashboardVM();
            employeeDashboardViewModel.TotalEmployeesNumber = db.Employees.Count();

            employeeDashboardViewModel.TotalMaleEmployeesNumber= db.Employees.Where(e => e.Gender.Equals("Male")).Count() ;

            employeeDashboardViewModel.TotalFemaleEmployeesNumber = db.Employees.Where(e => e.Gender.Equals("Female")).Count();

            DateTime testLessThanDate = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0));
            employeeDashboardViewModel.TotalEmpNumberPFTostartFor = db.Employees.Where(e => e.PFStatus == false &&  e.PFStartDate<= testLessThanDate ).Count();

            DateTime testLessThanDate1 = DateTime.Now.Add(new TimeSpan(120, 0, 0, 0));
            employeeDashboardViewModel.TotalEmpGratuityPending = db.Employees.Where(e => e.GratuityStatus == false && e.GratuityStartDate <= testLessThanDate1).Count();
            // Get employees for whom PF needs to be started
            //var probationOverEmployees = db.Employees.Where(e => System.Data.Objects.EntityFunctions.AddMonths(e.DateOfJoining.Value.
            //                                AddMonths(Convert.ToInt32(e.ProbationPeriod.Value)).Equals(DateTime.Today));

            var empList = db.Employees.ToList();
            var e1 = db.Employees.ToList()[0];
            var exp = System.DateTime.Today - e1.DateOfJoining.Value;
            var result = from e in db.Employees
                         group e.Id by new { experience = DbFunctions.DiffYears(e.DateOfJoining.Value, System.DateTime.Today) }
into g
                         select new { g.Key.experience, CountOf = g.Count() };

            var resultList = result.ToList();

            employeeDashboardViewModel.EmpServiceCount = resultList.Count();

            return View(employeeDashboardViewModel);
        }
    }
}