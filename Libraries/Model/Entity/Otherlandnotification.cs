using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public  class Otherlandnotification : AuditableEntity<int>
    {
        public Otherlandnotification()
        {
            Booktransferland = new HashSet<Booktransferland>();
            Ldoland = new HashSet<Ldoland>();
            Morland = new HashSet<Morland>();
        }
        [Required(ErrorMessage = "Land Type is mandatory")]
        public string LandType { get; set; }
        [Required(ErrorMessage = "Notification Number is mandatory")]
        public string NotificationNumber { get; set; }
      
        public byte? IsActive { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Ldoland> Ldoland { get; set; }
        public ICollection<Morland> Morland { get; set; }

    }
}
