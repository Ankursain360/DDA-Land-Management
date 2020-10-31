using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Menu : AuditableEntity<int>
    {

        [Required(ErrorMessage = "The Module Name field is required")]
        public int? ModuleId { get; set; }
        [Required]
        [Remote(action: "ExistName", controller: "Menu", AdditionalFields = "Id,ModuleId")]
        public string Name { get; set; }
        [Required]
        public int? SortBy { get; set; }
        [Required]
        public int? ParentMenuId { get; set; }
        
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Module> modulelist { get; set; }

        public virtual Module Module { get; set; }
    }
}
