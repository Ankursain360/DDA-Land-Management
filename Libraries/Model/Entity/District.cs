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
            Damagepayeeregister = new HashSet<Damagepayeeregister>();
            Userprofile = new HashSet<Userprofile>();
          
        }
        [Required(ErrorMessage = "District name is required")]
        [Remote(action: "Exist", controller: "Department", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "District code is Mandatory Field")]

        public string Code { get; set; }
        [Required]
        public byte IsActive { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<Acquiredlandvillage> Acquiredlandvillage { get; set; }
        public ICollection<Damagepayeeregister> Damagepayeeregister { get; set; }
       

    }
}
