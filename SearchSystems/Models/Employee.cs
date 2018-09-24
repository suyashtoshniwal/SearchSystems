//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchSystems.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal DepartmentId { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OfficeEmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Designation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public Nullable<int> ProbationPeriod { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PFStartDate { get; set; }
        public string PFAccountNumber { get; set; }
        public string PFUANNumber { get; set; }
        public string GratuityNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> GratuityStartDate { get; set; }
        public string MedicalInsuranceNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> InsuranceExpiryDate { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankIFSCCode { get; set; }
        public string AadharNumber { get; set; }
        public string PANNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string VehicleNumber { get; set; }
        public bool PFStatus { get; set; }
        public bool GratuityStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> InsuranceRenewalDate { get; set; }
        public Nullable<int> YearsOfExperince { get; set; }
        public string IsAccomodationProvided { get; set; }
        public string InsuranceBankName { get; set; }
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> LastWorkingDay { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime SalaryAppliedDate { get; set; }
        public int EmployerPFAmount { get; set; }
        public int EmpolyeePFAmount { get; set; }
        public int MonthlyGratuityAmount { get; set; }
        public int MonthlyInsuranceAmount { get; set; }
        public int MonthlyHousingAllowance { get; set; }
        public int TotalSalary { get; set; }
        public double EmployeePFPercentage { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
