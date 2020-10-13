using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    
    public class Zone : AuditableEntity<int>
    {
        public Zone()
        {
            Userprofile = new HashSet<Userprofile>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            LandTransfer = new HashSet<Landtransfer>();
            Locality = new HashSet<Locality>();
        }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Remote(action: "Exist", controller: "Zone", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required]
        [Remote(action: "IsCodeExist", controller: "Zone", AdditionalFields = "Id")]
        public string Code { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public string DepartmentName { get; set; }

        public virtual Department  Department{ get; set; }
        public virtual ICollection<Village> Village { get; set; }

        [NotMapped]
        public virtual ICollection<Locality> Locality { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public virtual ICollection<Landtransfer> LandTransfer { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; internal set; }
        public virtual ICollection<Userprofile> Userprofile { get; set; }
    }
}