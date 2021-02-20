


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class LandNotification : AuditableEntity<int>
    {
        public LandNotification()
        {
            Booktransferland = new HashSet<Booktransferland>();
            Ldoland = new HashSet<Ldoland>();
           // Morland = new HashSet<Morland>();
            Newlandus4plot = new HashSet<Newlandus4plot>();
        }

        [Required(ErrorMessage = " Notification is mandatory")]
        [Remote(action: "Exist", controller: "Notification", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte IsActive { get; set; }

        public virtual ICollection<Morland> Morland { get; set; }


        //public ICollection<Ldoland> Propertyregistration { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Ldoland> Ldoland { get; set; }
        public ICollection<Newlandus4plot> Newlandus4plot { get; set; }

    }
}

