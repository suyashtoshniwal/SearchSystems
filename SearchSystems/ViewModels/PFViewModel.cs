using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SearchSystems.ViewModels
{
    public class PFViewModel : WarningsViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PFStartDate { get; set; }

        public bool PFStatus { get; set; }

        public Nullable<int> ProbationPeriod { get; set; }
    }
}