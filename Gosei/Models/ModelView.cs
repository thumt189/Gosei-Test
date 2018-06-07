using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gosei.Models
{
    public class ModelView
    {
        public ModelEmployees Employee { get; set; }
        public List<ModelEmployeeQua> EmployeeQualifis { get; set; }
        public ModelEmployeeQua EmpQualifi { get; set; }
        public ModelQualifi Qualifi { get; set; }

    }
}