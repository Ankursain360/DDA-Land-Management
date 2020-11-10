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
            Menuactionrolemap = new HashSet<Menuactionrolemap>();
        }

        [Required(ErrorMessage = "Menu name field is mandatory")]
        public int? MenuId { get; set; }

        [Required (ErrorMessage = "Name field is mandatory")]       
        [Remote(action: "Exist", controller: "Page", AdditionalFields = "Id,MenuId")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url field is mandatory")]
        public string Url { get; set; }
       
        public byte? IsActive { get; set; }

        [NotMapped]
        //public List<Module> ModuleList { get; set; }

        //public virtual Module Module { get; set; }

        public List<Menu> MenuList { get; set; }

        public Menu Menu { get; set; }
        public ICollection<Menuactionrolemap> Menuactionrolemap { get; set; }
    }
}
