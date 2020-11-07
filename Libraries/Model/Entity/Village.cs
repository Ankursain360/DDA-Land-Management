using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Village : AuditableEntity<int>
    {
        public Village()
        {
          
            Nazul = new HashSet<Nazul>();
            
        }

        [Required(ErrorMessage = "The Zone field is required")]
        public int ZoneId { get; set; }
        [Required]
        [Remote(action: "Exist", controller: "Village", AdditionalFields = "Id")]
        public string Name { get; set; }
        public byte IsActive { get; set; } 
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [Required(ErrorMessage = "Division name is required")]
        [NotMapped]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "The Department field is required")]
        [NotMapped]
        public int DepartmentId { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<Nazul> Nazul { get; set; }
       
    }
}