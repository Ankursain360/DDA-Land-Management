using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Approvalstatus : AuditableEntity<int>
    {
        public Approvalstatus()
        {
            Approvalproccess = new HashSet<Approvalproccess>();
            Leaseapplication = new HashSet<Leaseapplication>();
            Watchandward = new HashSet<Watchandward>();
        }

        public int StatusCode { get; set; }

        [Required(ErrorMessage = "Approval Status is mandatory")]
        public string Name { get; set; }
        public string SentStatusName { get; set; }
        public int IsActive { get; set; }

        public ICollection<Approvalproccess> Approvalproccess { get; set; }
        public ICollection<Leaseapplication> Leaseapplication { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }
    }
}
