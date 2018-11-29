using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp1.Models
{
    public class PerformanceViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public List<RatingViewModel> Reviews { get; set; }
    }
}