using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Division : AuditableEntity<int>
    {
        public Division()
        {
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            LandtransferHandedOverDivision = new HashSet<Landtransfer>();
            LandtransferTakenOverDivision = new HashSet<Landtransfer>();
            Locality = new HashSet<Locality>();
            Nazulland = new HashSet<Nazulland>();
            //   Propertyregistration = new HashSet<Propertyregistration>();
            PropertyregistrationDivision = new HashSet<Propertyregistration>();
            PropertyregistrationHandedOverDivision = new HashSet<Propertyregistration>();
            PropertyregistrationTakenOverDivision = new HashSet<Propertyregistration>();
        }

        [Required(ErrorMessage = "Division name is required")]
        [Remote(action: "Exist", controller: "Division", AdditionalFields = "Id")]

        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public byte IsActive { get; set; }
        [Required(ErrorMessage = "The Zone field is required")]

        public int ZoneId { get; set; }
        [Required(ErrorMessage = "The Department field is required")]

        public int DepartmentId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual Department Department { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
      //  public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public virtual ICollection<Landtransfer> LandtransferHandedOverDivision { get; set; }
        public virtual ICollection<Landtransfer> LandtransferTakenOverDivision { get; set; }
        public virtual ICollection<Locality> Locality { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public ICollection<Nazulland> Nazulland { get; set; }
        public ICollection<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }

        public ICollection<Propertyregistration> PropertyregistrationDivision { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationHandedOverDivision { get; set; }
        public ICollection<Propertyregistration> PropertyregistrationTakenOverDivision { get; set; }
    }
}
