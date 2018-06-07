using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gosei.Models
{
    public class ModelEmployeeQua
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int QualificationId { get; set; }
        public string Institution { get; set; }
        public string City { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ValidFrom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ValidTo { get; set; }
        public string Note { get; set; }

        public string QuaName { get; set; }

    }
}