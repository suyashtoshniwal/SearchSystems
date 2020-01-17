using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystems.ViewModels
{
    public class EmployeeDashboardVHWViewModel
    {
       

        public int TotalVHWEmployeesNumberActive { get; set; }

        public int TotalVHWEmployeesNumberTotal { get; set; }

        public int TotalVHWMaleEmployeesNumberActive { get; set; }

        public int TotalVHWFemaleEmployeesNumberActive { get; set; }


      
        public int PastEmployees { get; set; }

        public int EmpServiceCount { get; set; }

        public IDictionary<int, int> EmployeeServiceDistribution = new Dictionary<int, int>();

        public IDictionary<int, int> EmployeeYearwiseDistribution = new Dictionary<int, int>();



        public DateTime LastUpdatedDate { get; set; }

        public IDictionary<string, int> BloodGroups = new Dictionary<string, int>();

        public List<object> ChartData = new List<object>();

        public IDictionary<string, DateTime?> EmployeeBirthdays = new Dictionary<string, DateTime?>();

        public List<DepartmentDistribution> DepartmentDistributions = new List<DepartmentDistribution>();
        public List<SalaryRangeMaleFemaleDistribution> SalaryRangeMaleFemaleDistributions = new List<SalaryRangeMaleFemaleDistribution>();

        public List<AgeWiseDistribution> AgeDistributions = new List<AgeWiseDistribution>();
        public List<EducationWiseDistribution> EducationDistributions = new List<EducationWiseDistribution>();

        public List<LastWorkOdDayDistribution> LastWorkODayDistributions = new List<LastWorkOdDayDistribution>();

    }

}