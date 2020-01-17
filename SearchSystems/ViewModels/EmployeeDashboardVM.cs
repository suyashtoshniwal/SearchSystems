using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystems.ViewModels
{
    public class EmployeeDashboardVM
    {
        public int TotalFullTimeEmployeesNumberActive { get; set; }

        public int TotalFullTimeEmployeesNumberTotal { get; set; }

        public int TotalFullTimeMaleEmployeesNumberActive { get; set; }

        public int TotalFullTimeFemaleEmployeesNumberActive { get; set; }

        public int TotalFullTimeMaleEmployeesNumberTotal { get; set; }

        public int TotalFullTimeFeMaleEmployeesNumberTotal { get; set; }

      


        public int TotalEmpNumberPFTostartFor { get; set; }
        public int TotalEmpGratuityPending { get; set; }

        public int TotalEmployeeInsurancePending { get; set; }

        public int TotalFDMaturing { get; set; }
        public int TotalClosedFD { get; set; }

        public int PastEmployees { get; set; }

        public int EmpServiceCount { get; set; }

        public IDictionary<int, int> EmployeeServiceDistribution = new Dictionary<int, int>();

        public IDictionary<int, int> EmployeeYearwiseDistribution = new Dictionary<int, int>();

        public IDictionary<int, int> EmployeeServiceDistributionTotal = new Dictionary<int, int>();

        public IDictionary<int, int> EmployeeYearwiseDistributionTotal = new Dictionary<int, int>();



        public DateTime LastUpdatedDate { get; set; }

        public IDictionary<string,int> BloodGroups = new Dictionary<string,int>();

        public List<object> ChartData = new List<object>();

        public IDictionary<string, DateTime?> EmployeeBirthdays = new Dictionary<string, DateTime?>();

        public List<DepartmentDistribution> DepartmentDistributions = new List<DepartmentDistribution>();
        public List<DepartmentDistribution> DepartmentDistributionsTotal = new List<DepartmentDistribution>();
        public List<SalaryRangeMaleFemaleDistribution> SalaryRangeMaleFemaleDistributions = new List<SalaryRangeMaleFemaleDistribution>();
        public List<SalaryRangeMaleFemaleDistribution> SalaryRangeMaleFemaleDistributionsTotal = new List<SalaryRangeMaleFemaleDistribution>();

        public List<AgeWiseDistribution>AgeDistributions = new List<AgeWiseDistribution>();
        public List<AgeWiseDistribution> AgeDistributionsTotal = new List<AgeWiseDistribution>();
        public List<EducationWiseDistribution> EducationDistributions = new List<EducationWiseDistribution>();
        public List<EducationWiseDistribution> EducationDistributionsTotal = new List<EducationWiseDistribution>();

        public List<LastWorkOdDayDistribution> LastWorkODayDistributions = new List<LastWorkOdDayDistribution>();

    }

    public class DepartmentDistribution
    {
        public string Name { get; set; }

        public int TotalEmployees { get; set; }

        public Nullable<int> TotalSalary { get; set; }

    }

    public class SalaryRangeMaleFemaleDistribution
    {
        public string SalaryRange { get; set; }

        public int TotalMaleEmployees { get; set; }

        public int TotalFemaleEmployees { get; set; }

    }

    public class AgeWiseDistribution
    {
        public string AgeRange { get; set; }

        public int TotalEmployees { get; set; }

    }

    public class EducationWiseDistribution
    {
        public string EducationRange { get; set; }

        public int TotalEmployees { get; set; }

    }

    public class LastWorkOdDayDistribution
    {
        public string LastWorkOdDayRange { get; set; }

        public int TotalEmployees { get; set; }

    }
}