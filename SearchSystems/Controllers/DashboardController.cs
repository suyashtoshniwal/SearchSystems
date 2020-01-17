using SearchSystems.Models;
using SearchSystems.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SearchSystems.Controllers
{
    public class DashboardController : Controller
    {
        private SEARCHSYSTEMSEntities4 db = new SEARCHSYSTEMSEntities4();
        private Entities fdDb = new Entities();

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
        [Authorize]
        public ActionResult Index(bool vhwView = false)
        {
            var employeeDashboardViewModel = new EmployeeDashboardVM();
            IQueryable<Employee> searchedEmployees = db.Employees.Where(employee => employee.IsActive == true && employee.VHW == "No");
            employeeDashboardViewModel.LastUpdatedDate = db.Employees.Max(employee => employee.UpdatedOn).Value;

            employeeDashboardViewModel.TotalFullTimeEmployeesNumberActive = db.Employees.Where(employee => employee.IsActive == true && employee.VHW == "No").Count();
            //employeeDashboardViewModel.TotalFullTimeEmployeesNumberActive = searchedEmployees.Count();
            employeeDashboardViewModel.TotalFullTimeEmployeesNumberTotal = db.Employees.Where(employee => employee.VHW == "No").Count();

            if (vhwView)
            {
                var employeeDashboardVHWModel = new EmployeeDashboardVHWViewModel();
                employeeDashboardVHWModel.TotalVHWEmployeesNumberActive = db.Employees.Where(employee => employee.IsActive == true && employee.VHW == "Yes").Count();
                employeeDashboardVHWModel.TotalVHWEmployeesNumberTotal = db.Employees.Where(employee => employee.VHW == "Yes").Count();
                return View("VHWIndex",employeeDashboardVHWModel);
            }

            employeeDashboardViewModel.TotalFullTimeMaleEmployeesNumberActive= searchedEmployees.Where(e => e.Gender.Equals("Male")).Count() ;

            employeeDashboardViewModel.TotalFullTimeFemaleEmployeesNumberActive = searchedEmployees.Where(e => e.Gender.Equals("Female")).Count();

            employeeDashboardViewModel.TotalFullTimeMaleEmployeesNumberTotal = db.Employees.Where(e => e.Gender.Equals("Male") && e.VHW == "No").Count();
            employeeDashboardViewModel.TotalFullTimeFeMaleEmployeesNumberTotal = db.Employees.Where(e => e.Gender.Equals("Female") && e.VHW == "No").Count();

            employeeDashboardViewModel.TotalFullTimeFemaleEmployeesNumberActive = searchedEmployees.Where(e => e.Gender.Equals("Female")).Count();


            employeeDashboardViewModel.PastEmployees = db.Employees.Where(e => e.IsActive == false).Count();

            DateTime testLessThanDate = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0));
            var pfStartDate = 
            employeeDashboardViewModel.TotalEmpNumberPFTostartFor = searchedEmployees.Where(e => e.PFStatus == false || SqlFunctions.DateAdd("month",e.ProbationPeriod, e.DateOfJoining) <= testLessThanDate ).Count();

            DateTime testLessThanDate1 = DateTime.Now.Add(new TimeSpan(120, 0, 0, 0));
            employeeDashboardViewModel.TotalEmpGratuityPending = searchedEmployees.Where(e => e.GratuityStatus == false || SqlFunctions.DateAdd("year",5, e.DateOfJoining) <= testLessThanDate).Count();

            DateTime testLessThanDate2 = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0));
            employeeDashboardViewModel.TotalEmployeeInsurancePending = searchedEmployees.Where(e => e.InsuranceRenewalDate<=testLessThanDate2 && e.MedicalInsuranceType == "NIC").Count();


            employeeDashboardViewModel.TotalFDMaturing = fdDb.FixedDeposits.Where(e => e.MaturityDate <= testLessThanDate && e.Status == true).Count();

            employeeDashboardViewModel.TotalClosedFD = fdDb.FixedDeposits.Where(e => e.Status == false).Count();
            // Get employees for whom PF needs to be started
            //var probationOverEmployees = db.Employees.Where(e => System.Data.Objects.EntityFunctions.AddMonths(e.DateOfJoining.Value.
            //                                AddMonths(Convert.ToInt32(e.ProbationPeriod.Value)).Equals(DateTime.Today));

            var oneYearExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 0 && e.YearsOfExperince < 1).ToList().Count();
            var twoYearExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 1 && e.YearsOfExperince < 2).ToList().Count();
            var threeYearExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 2 && e.YearsOfExperince < 3).ToList().Count();
            var fiveYearExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 3 && e.YearsOfExperince < 5).ToList().Count();
            var sevenYearExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 5 && e.YearsOfExperince < 7).ToList().Count();
            var nineExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 7 && e.YearsOfExperince < 9).ToList().Count();
            var elevenExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 9 && e.YearsOfExperince < 11).ToList().Count();
            var thirteenExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 11 && e.YearsOfExperince < 13).ToList().Count();
            var fifteenExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 13 &&
            e.YearsOfExperince < 15).ToList().Count();
            var aboveFifteenExperienceCount = searchedEmployees.Where(e => e.YearsOfExperince >= 15).ToList().Count();

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


            //Total

            IQueryable<Employee> searchedEmployeesTotal = db.Employees;

            var oneYearExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 0 && e.YearsOfExperince < 1).ToList().Count();
            var twoYearExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 1 && e.YearsOfExperince < 2).ToList().Count();
            var threeYearExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 2 && e.YearsOfExperince < 3).ToList().Count();
            var fiveYearExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 3 && e.YearsOfExperince < 5).ToList().Count();
            var sevenYearExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 5 && e.YearsOfExperince < 7).ToList().Count();
            var nineExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 7 && e.YearsOfExperince < 9).ToList().Count();
            var elevenExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 9 && e.YearsOfExperince < 11).ToList().Count();
            var thirteenExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 11 && e.YearsOfExperince < 13).ToList().Count();
            var fifteenExperienceCountTotal = searchedEmployeesTotal.Where(e => e.YearsOfExperince >= 13 &&
            e.YearsOfExperince < 15).ToList().Count();
            var aboveFifteenExperienceCountTotal = searchedEmployees.Where(e => e.YearsOfExperince >= 15).ToList().Count();

            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(1, oneYearExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(2, twoYearExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(3, threeYearExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(5, fiveYearExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(7, sevenYearExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(9, nineExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(11, elevenExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(13, thirteenExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(15, fifteenExperienceCountTotal);
            employeeDashboardViewModel.EmployeeServiceDistributionTotal.Add(16, aboveFifteenExperienceCountTotal);




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

            var fiveToTensalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 5000 && e.TotalSalary < 10000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var tenToFifteensalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 10000 && e.TotalSalary < 15000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var fifteenToTwentysalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 15000 && e.TotalSalary < 20000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var twentyToTwentyFivesalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 20000 && e.TotalSalary <25000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var twentyfiveToThirtysalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 25000 && e.TotalSalary < 30000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var thirtyToThirtyFiveSalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 30000 && e.TotalSalary < 35000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var thirtyfiveToFortySalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 35000 && e.TotalSalary < 40000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var fortyToFortyFiveSalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 40000 && e.TotalSalary < 45000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var fortyfiveToFiftySalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 45000 && e.TotalSalary < 50000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var aboveFiftySalaryMales = searchedEmployees.Where(e => e.TotalSalary >= 50000 && e.Gender.Equals("male", StringComparison.OrdinalIgnoreCase)).ToList().Count();

            var fiveToTensalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 5000 && e.TotalSalary < 10000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var tenToFifteensalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 10000 && e.TotalSalary < 15000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var fifteenToTwentysalarFeMales = searchedEmployees.Where(e => e.TotalSalary >= 15000 && e.TotalSalary < 20000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var twentyToTwentyFivesalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 20000 && e.TotalSalary < 25000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var twentyfiveToThirtysalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 25000 && e.TotalSalary < 30000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var thirtyToThirtyFiveSalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 30000 && e.TotalSalary < 35000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var thirtyfiveToFortySalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 35000 && e.TotalSalary < 40000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var fortyToFortyFiveSalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 40000 && e.TotalSalary < 45000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var fortyfiveToFiftySalaryFeMales = searchedEmployees.Where(e => e.TotalSalary >= 45000 && e.TotalSalary < 50000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();
            var aboveFiftySalaryFemales = searchedEmployees.Where(e => e.TotalSalary >= 50000 && e.Gender.Equals("female", StringComparison.OrdinalIgnoreCase)).ToList().Count();


            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "5000 to 10000", TotalMaleEmployees = fiveToTensalaryMales, TotalFemaleEmployees = fiveToTensalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "10000 to 15000", TotalMaleEmployees = tenToFifteensalaryMales, TotalFemaleEmployees = tenToFifteensalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "15000 to 20000", TotalMaleEmployees = fifteenToTwentysalaryMales, TotalFemaleEmployees = fifteenToTwentysalarFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "20000 to 25000", TotalMaleEmployees = twentyToTwentyFivesalaryMales, TotalFemaleEmployees = twentyToTwentyFivesalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "25000 to 30000", TotalMaleEmployees = twentyfiveToThirtysalaryMales, TotalFemaleEmployees = twentyfiveToThirtysalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "30000 to 35000", TotalMaleEmployees = thirtyToThirtyFiveSalaryMales, TotalFemaleEmployees = thirtyToThirtyFiveSalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "35000 to 40000", TotalMaleEmployees = thirtyfiveToFortySalaryMales, TotalFemaleEmployees = thirtyfiveToFortySalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "40000 to 45000", TotalMaleEmployees = fortyToFortyFiveSalaryMales, TotalFemaleEmployees = fortyfiveToFiftySalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "45000 to 50000", TotalMaleEmployees = fortyfiveToFiftySalaryMales, TotalFemaleEmployees = fortyfiveToFiftySalaryFeMales });
            employeeDashboardViewModel.SalaryRangeMaleFemaleDistributions.Add(new SalaryRangeMaleFemaleDistribution() { SalaryRange = "> 50000", TotalMaleEmployees = aboveFiftySalaryMales, TotalFemaleEmployees = aboveFiftySalaryFemales });

            var twentyToThirtyAgeEmployeeCount = searchedEmployees.Where(employee => employee.Age >= 20 && employee.Age <= 20).Count();
            var thirtyToFortyAgeEmployeeCount = searchedEmployees.Where(employee => employee.Age >= 30 && employee.Age <= 40).Count();
            var fortyToFiftyAgeEmployeeCount = searchedEmployees.Where(employee => employee.Age >= 40 && employee.Age <= 50).Count();
            var fiftyToSixtyAgeEmployeeCount = searchedEmployees.Where(employee => employee.Age >= 50 && employee.Age <= 60).Count();
            var sixtyToSeventyAgeEmployeeCount = searchedEmployees.Where(employee => employee.Age >= 60 && employee.Age <= 70).Count();
            var seventyToEightyAgeEmployeeCount = searchedEmployees.Where(employee => employee.Age >= 70 && employee.Age <= 80).Count();

            employeeDashboardViewModel.AgeDistributions.Add(new AgeWiseDistribution() { AgeRange = "20 to 30", TotalEmployees = twentyToThirtyAgeEmployeeCount });
            employeeDashboardViewModel.AgeDistributions.Add(new AgeWiseDistribution() { AgeRange = "30 to 40", TotalEmployees = thirtyToFortyAgeEmployeeCount });
            employeeDashboardViewModel.AgeDistributions.Add(new AgeWiseDistribution() { AgeRange = "40 to 50", TotalEmployees = fortyToFiftyAgeEmployeeCount });
            employeeDashboardViewModel.AgeDistributions.Add(new AgeWiseDistribution() { AgeRange = "50 to 60", TotalEmployees = fiftyToSixtyAgeEmployeeCount });
            employeeDashboardViewModel.AgeDistributions.Add(new AgeWiseDistribution() { AgeRange = "60 to 70", TotalEmployees = sixtyToSeventyAgeEmployeeCount});
            employeeDashboardViewModel.AgeDistributions.Add(new AgeWiseDistribution() { AgeRange = "70 to 80", TotalEmployees = seventyToEightyAgeEmployeeCount });

            var query4 =
           from employee in db.Employees
           where employee.IsActive == false
           group employee by employee.LastWorkingDay.Value.Year into e
           select new LastWorkOdDayDistribution { LastWorkOdDayRange = e.Key.ToString(), TotalEmployees = e.Count() };

            var query1 =
             from employee in db.Employees
             where employee.IsActive == true
             group employee by employee.Education into e
             select new EducationWiseDistribution { EducationRange = e.Key, TotalEmployees = e.Count()};


            var query =
              from employee in db.Employees join department in db.Departments
              on employee.DepartmentId equals department.Id
              where employee.IsActive == true
              group employee by employee.DepartmentId into e
              select new DepartmentDistribution { Name = e.FirstOrDefault(e1 => e1.Department.Name.Length > 0).Department.Name, TotalEmployees = e.Count(), TotalSalary = e.Sum(e1 => e1.TotalSalary) };

            var bloodGroups = db.Employees.Where(bg => bg.IsActive == true).GroupBy(BloodGroups => BloodGroups.BloodGroup)
                .Select(g => new { Name = g.Key, Count = g.Count() }).ToDictionary(g => g.Name, g => g.Count);

            employeeDashboardViewModel.BloodGroups = bloodGroups;

            var dateMonthfromToday = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0));
           
            var employeeBirthdays = db.Employees.Where(employee => employee.IsActive == true &&  (dateMonthfromToday.DayOfYear - SqlFunctions.DatePart("dayofyear", employee.DOB)) >= 0 &&
            (dateMonthfromToday.DayOfYear - SqlFunctions.DatePart("dayofyear", employee.DOB)) <= 30)
                .ToDictionary(ed => String.Concat(ed.FirstName," ", ed.LastName), ed => ed.DOB);
            employeeDashboardViewModel.EmployeeBirthdays = employeeBirthdays;

            employeeDashboardViewModel.DepartmentDistributions = query.ToList();

            employeeDashboardViewModel.EducationDistributions = query1.ToList();

            employeeDashboardViewModel.LastWorkODayDistributions = query4.ToList();

            return View(employeeDashboardViewModel);
        }
    }
}