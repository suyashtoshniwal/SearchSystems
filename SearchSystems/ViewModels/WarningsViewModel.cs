using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystems.ViewModels
{
    public class WarningsViewModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public decimal DepartmentId { get; set; }
        public string Gender { get; set; }

        public Nullable<System.DateTime> DateOfJoining { get; set; }

        public string DepartmentName { get; set; }

        public bool IsAccountsLogin { get; set; }

        public bool IsAdminLogin { get; set; }

    }
}