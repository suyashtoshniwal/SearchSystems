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

            var oneYearExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 0 && e.YearsOfExperince < 1).ToList().Count();
            var twoYearExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 1 && e.YearsOfExperince < 2).ToList().Count();
            var threeYearExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 2 && e.YearsOfExperince < 3).ToList().Count();
            var fiveYearExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 3 && e.YearsOfExperince < 5).ToList().Count();
            var sevenYearExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 5 && e.YearsOfExperince < 7).ToList().Count();
            var nineExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 7 && e.YearsOfExperince < 9).ToList().Count();
            var elevenExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 9 && e.YearsOfExperince < 11).ToList().Count();
            var thirteenExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 11 && e.YearsOfExperince < 13).ToList().Count();
            var fifteenExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 13 && e.YearsOfExperince < 15).ToList().Count();
            var aboveFifteenExperienceCount = db.Employees.Where(e => e.YearsOfExperince >= 15).ToList().Count();

            employeeDashboardViewModel.EmployeeServiceDistribution.Add(1, oneYearExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(2, twoYearExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(3, threeYearExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(5, fiveYearExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(7, sevenYearExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(9, nineExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(11, elevenExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(13, thirteenExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(15, fifteenExperienceCount);
            employeeDashboardViewModel.EmployeeServiceDistribution.Add(16, aboveFifteenExperienceCount);

            var startingDate = new DateTime(2001, 01, 01);
            var endingDate = new DateTime(2005, 12, 31);
            var twoToFive = db.Employees.Where(e => e.DateOfJoining >= startingDate  && e.DateOfJoining <= endingDate).ToList().Count();

            startingDate = new DateTime(2006, 01, 01);
            endingDate = new DateTime(2010, 12, 31);
            var fiveToTen = db.Employees.Where(e => e.DateOfJoining >= startingDate && e.DateOfJoining <= endingDate).ToList().Count();

            startingDate = new DateTime(2011, 01, 01);
            endingDate = new DateTime(2015, 12, 31);
            var elevenToFifteen = db.Employees.Where(e => e.DateOfJoining >= startingDate && e.DateOfJoining <= endingDate).ToList().Count();

            startingDate = new DateTime(2016, 01, 01);
            endingDate = new DateTime(2020, 12, 31);
            var sixteenToTweety = db.Employees.Where(e => e.DateOfJoining >= startingDate && e.DateOfJoining <= endingDate).ToList().Count();

            startingDate = new DateTime(1990, 12, 31);
            var beforeNinty = db.Employees.Where(e => e.DateOfJoining <= startingDate).ToList().Count();

            startingDate = new DateTime(1991, 01, 01);
            endingDate = new DateTime(1995, 12, 31);
            var nintyOneToFive = db.Employees.Where(e => e.DateOfJoining >= startingDate && e.DateOfJoining <= endingDate).ToList().Count();

            startingDate = new DateTime(1996, 01, 01);
            endingDate = new DateTime(2000, 12, 31);
            var nintyFiveToZero = db.Employees.Where(e => e.DateOfJoining >= startingDate && e.DateOfJoining <= endingDate).ToList().Count();

            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(1, twoToFive);
            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(2, fiveToTen);
            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(3, elevenToFifteen);
            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(4, sixteenToTweety);
            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(5, beforeNinty);
            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(6, nintyOneToFive);
            employeeDashboardViewModel.EmployeeYearwiseDistribution.Add(7, nintyFiveToZero);

            var query =
              from employee in db.Employees join department in db.Departments
              on employee.DepartmentId equals department.Id
              group employee by employee.DepartmentId into e
              select new DepartmentDistribution { Name = e.FirstOrDefault(e1 => e1.Department.Name.Length > 0).Department.Name, TotalEmployees = e.Count(), TotalSalary = e.Sum(e1 => e1.Salary) };

            var bloodGroups = db.Employees.GroupBy(BloodGroups => BloodGroups.BloodGroup)
                .Select(g => new { Name = g.Key, Count = g.Count() }).ToDictionary(g => g.Name, g => g.Count);

            employeeDashboardViewModel.BloodGroups = bloodGroups;

            employeeDashboardViewModel.DepartmentDistributions = query.ToList();

            return View(employeeDashboardViewModel);
        }
    }
}