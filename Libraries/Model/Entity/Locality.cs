using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Locality:AuditableEntity<int>
    {
        public Locality()
        {
            Booktransferland = new HashSet<Booktransferland>();
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Watchandward = new HashSet<Watchandward>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            Landtransfer = new HashSet<Landtransfer>();
        }
        [Required(ErrorMessage = "The Department field is required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The Zone field is required")]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = "The Division field is required")]
        public int DivisionId { get; set; }
        [Required]
        [Remote(action: "ExistName", controller: "Locality", AdditionalFields = "Id")]
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
        public ICollection<Landtransfer> Landtransfer { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public ICollection<Khasra> Khasra { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }

    }
}
