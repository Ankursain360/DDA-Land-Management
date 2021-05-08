using Dto.Master;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Libraries.Model.Entity
{
    public class Module : AuditableEntity<int>
    {
        public Module()
        {
            Menu = new HashSet<Menu>();
            Menuactionrolemap = new HashSet<Menuactionrolemap>();
            WorkflowTemplate = new HashSet<WorkflowTemplate>();
        }

        [Required(ErrorMessage = " Module name is mandatory")]
        [Remote(action: "Exist", controller: "Module", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sort By field is mandatory")]
        public int SortBy { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Target { get; set; }
        public string Guid { get; set; }

        [Required(ErrorMessage = "Module Category is mandatory", AllowEmptyStrings = false)]
        public int? ModuleCategoryId { get; set; }

        public ModuleCategory ModuleCategory { get; set; }
        public ICollection<WorkflowTemplate> WorkflowTemplate { get; set; }
        public ICollection<Onlinecomplaint> Onlinecomplaint { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<Menuactionrolemap> Menuactionrolemap { get; set; }

        [NotMapped]
        public List<ModuleCategory> ModuleCategoryList { get; set; }
    }
}

