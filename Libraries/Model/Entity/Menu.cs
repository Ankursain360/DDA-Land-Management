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
            Page = new HashSet<Page>();
        }

        public int? ModuleId { get; set; }
        public string Name { get; set; }
        public int? SortBy { get; set; }
        public int? ParentMenuId { get; set; }
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<Module> modulelist { get; set; }

        public virtual Module Module { get; set; }

        public ICollection<Page> Page { get; set; }
    }
}
