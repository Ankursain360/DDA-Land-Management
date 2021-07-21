using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Kycapprovalproccess : AuditableEntity<int>
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
