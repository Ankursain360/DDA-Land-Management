using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class WorkflowTemplate : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModuleId { get; set; }
        public string Template { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Module> ModuleList { get; set; }

        [NotMapped]
        public List<int?> WorkflowIdList { get; set; }
        [NotMapped]
        public List<string> ParameterNameList { get; set; }
        [NotMapped]
        public List<string> ParameterValueList { get; set; }
        [NotMapped]
        public List<string> ParameterLevelList { get; set; }
        [NotMapped]
        public List<string> ParameterSkipList { get; set; }

        [NotMapped]
        public bool IsActiveData { get; set; }

        public Module Module { get; set; }
    }
}
