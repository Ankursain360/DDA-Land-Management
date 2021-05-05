using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Approvalurltemplatemapping : AuditableEntity<int>
    {
        public int ModuleId { get; set; }
        public string ProcessGuid { get; set; }
        public string SubModuleUrl { get; set; }
        public string SubModuleUrlLocal { get; set; }
        public byte IsActive { get; set; }
    }
}
