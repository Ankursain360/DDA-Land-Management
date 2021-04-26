


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
            Newlandus17plot = new HashSet<Newlandus17plot>();
            Newlandus6plot = new HashSet<Newlandus6plot>();
            Newlandus22plot = new HashSet<Newlandus22plot>();
           
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
        public ICollection<Newlandus17plot> Newlandus17plot { get; set; }
        public ICollection<Newlandus6plot> Newlandus6plot { get; set; }
        public ICollection<Newlandus22plot> Newlandus22plot { get; set; }
        
    }
}

