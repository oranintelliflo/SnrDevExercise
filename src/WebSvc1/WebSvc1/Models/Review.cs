using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebSvc1.Models
{
    [Serializable]
    [XmlRoot(ElementName = "review")]
    public class Review
    {
        [XmlElement(ElementName = "EmployeeId", DataType = "int")]
        public int EmployeeId { get; set; }

        [XmlElement(ElementName = "Competency")]
        public string Competency { get; set; }

        [XmlElement(ElementName = "CompetencyRating", DataType = "int")]
        public int CompetencyRating { get; set; }

        [XmlElement(ElementName = "Comments")]
        public string Comments { get; set; }
    }
}