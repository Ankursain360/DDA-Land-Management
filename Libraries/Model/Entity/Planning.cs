using Libraries.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Planning : AuditableEntity<int>
    {
        public Planning()
        {
            PlanningProperties = new HashSet<PlanningProperties>();
        }
       
        [Required(ErrorMessage = "Remarks is Mandatory")]
        public string Remarks { get; set; }
        public byte? IsVerify { get; set; }
        public byte? IsActive { get; set; }
        public virtual ICollection<PlanningProperties> PlanningProperties { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        
        [Required(ErrorMessage = "Department Name is Mandatory")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Division Name is Mandatory")]
        public int DivisionId { get; set; }
        [NotMapped]
        public List<int> PlannedProperties { get; set; }
        [NotMapped]
        public List<int> UnplannedProperties { get; set; }
        [Required(ErrorMessage = "Zone Name is Mandatory")]
        public int ZoneId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Zone Zone { get; set; }
        [NotMapped]
        public List<Propertyregistration> PlannedList { get; set; }
        [NotMapped]
        public List<Propertyregistration> UnplannedList { get; set; }
    }
}
