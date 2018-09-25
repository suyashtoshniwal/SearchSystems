﻿using System;
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

        public int TotalFDMaturing { get; set; }
        public int TotalClosedFD { get; set; }

        public int PastEmployees { get; set; }

        public int EmpServiceCount { get; set; }

        public IDictionary<int, int> EmployeeServiceDistribution = new Dictionary<int, int>();

        public IDictionary<int, int> EmployeeYearwiseDistribution = new Dictionary<int, int>();

        public IDictionary<string,int> BloodGroups = new Dictionary<string,int>();

        public List<object> ChartData = new List<object>();

        public IDictionary<string, DateTime?> EmployeeBirthdays = new Dictionary<string, DateTime?>();

        public List<DepartmentDistribution> DepartmentDistributions = new List<DepartmentDistribution>();

    }

    public class DepartmentDistribution
    {
        public string Name { get; set; }

        public int TotalEmployees { get; set; }

        public Nullable<int> TotalSalary { get; set; }

    }
}