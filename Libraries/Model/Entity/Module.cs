using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Module : AuditableEntity<int>
    {
        public Module()
        {
          //  Menu = new HashSet<Menu>();
            Rolemodulemap = new HashSet<Rolemodulemap>();
        }

        [Required]
        [Remote(action: "Exist", controller: "Module", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        public byte? IsActive { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Target { get; set; }
      
        public ICollection<WorkflowTemplate> WorkflowTemplate { get; set; }
          public ICollection<Onlinecomplaint> Onlinecomplaint { get; set; }
        public ICollection<Menu> Menu { get; set; }
        public ICollection<Rolemodulemap> Rolemodulemap { get; set; }
    }
   
}
