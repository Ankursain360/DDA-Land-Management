using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Kycworkflowtemplate : AuditableEntity<int>
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModuleId { get; set; }
        public string Version { get; set; }
        public int Slatime { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Template { get; set; }
        public byte IsActive { get; set; }
        public string ProcessGuid { get; set; }
        

        public Module Module { get; set; }
    }
}
