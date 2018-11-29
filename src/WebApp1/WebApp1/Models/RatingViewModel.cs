using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp1.Models
{
    public class RatingViewModel
    {
        public int EmployeeId { get; set; }
        
        public string Competency { get; set; }

        public int CompetencyRating { get; set; }

        public string Comments { get; set; }
    }
}