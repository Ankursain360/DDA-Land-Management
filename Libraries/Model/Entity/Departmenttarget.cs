using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Departmenttarget : AuditableEntity<int>
    {

        public Departmenttarget()
        {
           
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is mandatory")]
        [Remote(action: "Exist", controller: "DepartmentTarget", AdditionalFields = "DepartmentId")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Files to be done is mandatory")]
        public int FilesToBeDone { get; set; }

        [Required(ErrorMessage = "Weeks to be done is mandatory")]
        public int WeeklyToBeDone { get; set; }
        public byte IsActive { get; set; }
        

        public Department Department { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }


    }
}
