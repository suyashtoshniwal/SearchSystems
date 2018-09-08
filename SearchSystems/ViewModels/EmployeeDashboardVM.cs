using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystems.ViewModels
{
    public class EmployeeDashboardVM
    {
        public int TotalEmployeesNumber { get; set; }

        public int TotalMaleEmployeesNumber { get; set; }

        public int TotalFemaleEmployeesNumber { get; set; }

        public int TotalEmpNumberPFTostartFor { get; set; }
        public int TotalEmpGratuityPending { get; set; }

        public int EmpServiceCount { get; set; }

        public List<object> ChartData = new List<object>();
   
    }
}