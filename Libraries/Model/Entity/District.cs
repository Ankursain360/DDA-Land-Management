
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
   public partial class District : AuditableEntity<int>
    {
        public District()
        {
            Acquiredlandvillage = new HashSet<Acquiredlandvillage>();
            Damagepayeeregister = new HashSet<Damagepayeeregister>();
            Newlandannexure1 = new HashSet<Newlandannexure1>();
            Newlandvillage = new HashSet<Newlandvillage>();
            Userprofile = new HashSet<Userprofile>();
            NewdamageSelfassessment = new HashSet<NewDamageSelfAssessment>();
            Newdamagepayeeregistration = new HashSet<Newdamagepayeeregistration>();
        }
        [Required(ErrorMessage = "District name is Mandatory")]
        [Remote(action: "Exist", controller: "District", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "District code is Mandatory ")]

        public string Code { get; set; }
        [Required]
        public byte IsActive { get; set; }

        public ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }
        public ICollection<Damagepayeeregister> Damagepayeeregister { get; set; }
        public ICollection<Newlandannexure1> Newlandannexure1 { get; set; }
        public ICollection<Newlandvillage> Newlandvillage { get; set; }
        public ICollection<Userprofile> Userprofile { get; set; }

        public ICollection<Newdamagepayeeregistration> Newdamagepayeeregistration { get; set; }
        public ICollection<NewDamageSelfAssessment> NewdamageSelfassessment { get; set; }
    }
}
