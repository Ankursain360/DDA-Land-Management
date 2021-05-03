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
            Damagepayeeregister = new HashSet<Damagepayeeregister>();
            Extension = new HashSet<Extension>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            Fixingdemolition = new HashSet<Fixingdemolition>();
            Leaseapplication = new HashSet<Leaseapplication>();
            Onlinecomplaint = new HashSet<Onlinecomplaint>();
            Request = new HashSet<Request>();
            Watchandward = new HashSet<Watchandward>();
        }

        public int StatusCode { get; set; }

        [Required(ErrorMessage = "Approval Status is mandatory")]
        public string Name { get; set; }
        public string SentStatusName { get; set; }
        public int IsActive { get; set; }


        public ICollection<Approvalproccess> Approvalproccess { get; set; }
        public ICollection<Damagepayeeregister> Damagepayeeregister { get; set; }
        public ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public ICollection<Extension> Extension { get; set; }
        public ICollection<Fixingdemolition> Fixingdemolition { get; set; }
        public ICollection<Leaseapplication> Leaseapplication { get; set; }
        public ICollection<Onlinecomplaint> Onlinecomplaint { get; set; }
        public ICollection<Request> Request { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }
    }
}
