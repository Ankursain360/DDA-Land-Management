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
        public WorkflowTemplate()
        {
            Processworkflow = new HashSet<Processworkflow>();
        }
        [Required(ErrorMessage = " Proccess Name is mandatory")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Proccess Description is mandatory")]
        public string Description { get; set; }
        [Required(ErrorMessage = " Module is mandatory")]
        public int ModuleId { get; set; }
        public string UserType { get; set; }
        public string Template { get; set; }
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Module> ModuleList { get; set; }

        [NotMapped]
        public List<int?> WorkflowIdList { get; set; }
        [NotMapped]
        public List<string> ParameterNameList { get; set; }

        [NotMapped]
        public List<string> ParameterLabelNameList { get; set; }
        [NotMapped]
        public List<string> ParameterValueList { get; set; }
        [NotMapped]
        public List<string> ParameterLevelList { get; set; }
        [NotMapped]
        public List<string> ParameterSkipList { get; set; }

        [NotMapped]
        public List<string> ParameterActionList { get; set; }

        [NotMapped]
        public bool IsActiveData { get; set; }

        public Module Module { get; set; }

        [NotMapped]
        public string OperationId { get; set; }

        public ICollection<Processworkflow> Processworkflow { get; set; }
    }
}
