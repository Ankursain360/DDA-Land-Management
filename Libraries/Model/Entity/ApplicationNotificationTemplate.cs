using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public class ApplicationNotificationTemplate : AuditableEntity<int>
    {
        public string UserNotificationGuid { get; set; }
        [Required(ErrorMessage = " Template name is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Template is mandatory")]
        public string Template { get; set; }
        public string URL { get; set; }
        public short? IsActive { get; set; } 
    }
}
