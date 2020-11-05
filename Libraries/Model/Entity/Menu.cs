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
        public Menu()
        {
            InverseParentMenu = new HashSet<Menu>();
            Page = new HashSet<Page>();
        }

        [Required(ErrorMessage = "The Module field is required")]
        public int? ModuleId { get; set; }

        [Required(ErrorMessage = "Name field in required")]
        [Remote(action: "Exist", controller: "Menu", AdditionalFields = "Id,ModuleId")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sort field in required")]
        public int? SortBy { get; set; }
      
        public int? ParentMenuId { get; set; }

        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Menu> parentmenulist { get; set; }

        [NotMapped]
        public List<Module> modulelist { get; set; }

        public virtual Module Module { get; set; }
        public Menu ParentMenu { get; set; }
        public ICollection<Menu> InverseParentMenu { get; set; }
        public ICollection<Page> Page { get; set; }
    }
}
