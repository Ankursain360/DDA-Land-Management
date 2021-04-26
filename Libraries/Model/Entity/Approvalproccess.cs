using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Approvalproccess : AuditableEntity<int>
    {
        public int ModuleId { get; set; }
        public string ProcessGuid { get; set; }
        public int ServiceId { get; set; }
        public string Version { get; set; }
        public int Level { get; set; }
        public string SendFrom { get; set; }
        public string SendTo { get; set; }
        public int? PendingStatus { get; set; }
        public int? Status { get; set; }
        public string Remarks { get; set; }
        public string DocumentName { get; set; }
        public string SendFromProfileId { get; set; }
        public string SendToProfileId { get; set; }

        public Approvalstatus StatusNavigation { get; set; }
    }
}
