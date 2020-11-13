using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
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
            Menuactionrolemap = new HashSet<Menuactionrolemap>();
            Page = new HashSet<Page>();
        }

        [Required(ErrorMessage = "Module Name field field is Mandatory")]
        public int? ModuleId { get; set; }

        [Required(ErrorMessage = "Menu Name field field is Mandatory")]
        [Remote(action: "Exist", controller: "Menu", AdditionalFields = "Id,ModuleId")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sort By field is Mandatory")]
        public int? SortBy { get; set; }
      
        public int? ParentMenuId { get; set; }

        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Menu> parentmenulist { get; set; }

        [NotMapped]
        public List<Module> modulelist { get; set; }

        public virtual Module Module { get; set; }
        public Menu ParentMenu { get; set; }
        public virtual ICollection<Menu> InverseParentMenu { get; set; }
        public virtual ICollection<Page> Page { get; set; }
        public virtual ICollection<Menuactionrolemap> Menuactionrolemap { get; set; }
    }
}
