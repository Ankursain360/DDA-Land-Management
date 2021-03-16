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
        public int ProccessID { get; set; }
        public int ServiceId { get; set; }
        public int? SendFrom { get; set; }
        public int? SendTo { get; set; }
        public int? PendingStatus { get; set; }
        public int? Status { get; set; }
        public string Remarks { get; set; }
        public string DocumentName { get; set; }
        //  public virtual ApplicationUser SendFromUser { get; set; }
    }
}
