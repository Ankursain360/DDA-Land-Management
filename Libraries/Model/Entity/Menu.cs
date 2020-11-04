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


       


        public int? ModuleId { get; set; }
        [Required(ErrorMessage = "The Module field is required")]
        [Remote(action: "ExistName", controller: "Menu", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Name field in required")]
        public int? SortBy { get; set; }
        [Required(ErrorMessage = "Sort field in required")]
      
        public int? ParentMenuId { get; set; }
        [Required]

        public byte? IsActive { get; set; }
        [Required]
        [NotMapped]
        public List<Module> modulelist { get; set; }

        public virtual Module Module { get; set; }

        public ICollection<Page> Page { get; set; }
    }
}
