using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Notification : AuditableEntity<int>
    {
        [Required]
        [Remote(action: "Exist", controller: "Notification", AdditionalFields = "Id")]
        public string Name { get; set; }
        public byte IsActive { get; set; }

    }
}

