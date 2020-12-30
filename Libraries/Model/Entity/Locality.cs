using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Locality : AuditableEntity<int>
    {
        public Locality()
        {
            

            Booktransferland = new HashSet<Booktransferland>();
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Watchandward = new HashSet<Watchandward>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            Propertyregistration = new HashSet<Propertyregistration>();
            MonthlyRoaster = new HashSet<MonthlyRoaster>();
            Damagepayeeregister = new HashSet<Damagepayeeregister>();
            Mutationdetails = new HashSet<Mutationdetails>();
            Damagepayeeregistertemp = new HashSet<Damagepayeeregistertemp>();
            Legalmanagementsystem = new HashSet<Legalmanagementsystem>();
        }
        [Required(ErrorMessage = "The Department field is required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The Zone field is required")]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = "The Division field is required")]
        public int DivisionId { get; set; }
        [Required]
        [Remote(action: "ExistName", controller: "Locality", AdditionalFields = "Id,DepartmentId,DivisionId,ZoneId")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Locality Code field is required")]
        [Remote(action: "ExistCode", controller: "Locality", AdditionalFields = "Id")]
        public string LocalityCode { get; set; }
        [Required]
        public string Landmark { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public byte IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Zone Zone { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public ICollection<Khasra> Khasra { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }
        public ICollection<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }
        public ICollection<MonthlyRoaster> MonthlyRoaster { get; set; }
        public ICollection<Damagepayeeregister> Damagepayeeregister { get; set; }
        public ICollection<Mutationdetails> Mutationdetails { get; set; }
        public ICollection<Damagepayeeregistertemp> Damagepayeeregistertemp { get; set; }

        public ICollection<Legalmanagementsystem> Legalmanagementsystem { get; set; }

    }
}
