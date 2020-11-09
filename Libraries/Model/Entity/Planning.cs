using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Planning :AuditableEntity<int>
    {
        public Planning()
        {
            PlanningProperties = new HashSet<PlanningProperties>();
        }
        [Required]
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public virtual ICollection<PlanningProperties> PlanningProperties { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [Required(ErrorMessage ="The Department Field is required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The Division Field is required")]
        public int DivisionId { get; set; }
        [NotMapped]
        public List<int> PlannedProperties { get; set; }
        [NotMapped]
        public List<int> UnplannedProperties { get; set; }
        [Required(ErrorMessage = "The Zone Field is required")]
        public int ZoneId{ get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Zone Zone { get; set; }
        [NotMapped]
        public List<Propertyregistration> PlannedList { get; set; }
        [NotMapped]
        public List<Propertyregistration> UnplannedList { get; set; }
    }
}
