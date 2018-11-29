using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSvc1.Models
{
    public class EmployeeReview
    {
        public Employee Employee { get; set; }
        public IList<Review> Reviews { get; set; }
    }
}