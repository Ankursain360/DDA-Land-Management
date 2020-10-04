using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Module : AuditableEntity<int>
    {

        [Required]
        [Remote(action: "Exist", controller: "Module", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        public byte? IsActive { get; set; }

        public ICollection<WorkflowTemplate> WorkflowTemplate { get; set; }
    }
}
