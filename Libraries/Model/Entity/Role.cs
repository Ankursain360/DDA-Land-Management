using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Role : AuditableEntity<int>
    {
        [Required(ErrorMessage = "The Role Name field is required")]
        //[Remote(action: "Exist", controller: "Role", AdditionalFields = "Id")]

        public string Name { get; set; }
        [Required(ErrorMessage = "The Active Status field is required")]

        public byte IsActive { get; set; }
        [NotMapped]
        public string ZoneName { get; set; }
        [NotMapped]
        public virtual ICollection<PageRole> PageRole { get; set; }
    }
}
