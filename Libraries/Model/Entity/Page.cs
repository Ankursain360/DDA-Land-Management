using Microsoft.AspNetCore.Mvc;
using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Page : AuditableEntity<int>
    {
        public Page()
        {
            Submenuactionrolemap = new HashSet<Submenuactionrolemap>();
        }

        [Required(ErrorMessage = "Menu name is required")]
        public int? MenuId { get; set; }
        [Required (ErrorMessage ="Page name is required")]
       
        [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please fill Url feild ")]
        public string Url { get; set; }
       
        public byte? IsActive { get; set; }

        [NotMapped]
        //public List<Module> ModuleList { get; set; }

        //public virtual Module Module { get; set; }

        public List<Menu> MenuList { get; set; }

        public Menu Menu { get; set; }
        public ICollection<Submenuactionrolemap> Submenuactionrolemap { get; set; }
    }
}
