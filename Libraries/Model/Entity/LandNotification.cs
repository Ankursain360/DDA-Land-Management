


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class LandNotification : AuditableEntity<int>
    {
       

        [Required(ErrorMessage = " Notification is mandatory")]
        [Remote(action: "Exist", controller: "Notification", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte IsActive { get; set; }

        //public virtual ICollection<Morland> Morland { get; set; }


        //public ICollection<Ldoland> Propertyregistration { get; set; }
       
     
    }
}

