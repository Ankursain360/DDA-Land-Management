using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class ModuleCategory : AuditableEntity<int>
    {
        public ModuleCategory()
        {
            Module = new HashSet<Module>();
        }

        [Required(ErrorMessage = "Category Name is Mandatory Field")]
        public string CategoryName { get; set; }      
        public byte? IsActive { get; set; }
        public ICollection<Module> Module { get; set; }
    }
}

