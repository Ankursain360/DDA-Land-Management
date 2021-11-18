using Microsoft.AspNetCore.Identity;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class ApplicationUser : IdentityUser<int> 
    {
        public ApplicationUser()
        {
            Userprofile = new HashSet<Userprofile>();
            Paymentverification = new HashSet<Paymentverification>();
            RefreshToken = new HashSet<RefreshToken>();
            // ApprovalproccessSendFromUser = new HashSet<Approvalproccess>();
        }
        
        public DateTime? PasswordSetDate { get; set; }
        public short? IsDefaultPassword { get; set; }
        public string Name { get; set; }
        public string ChangePasswordStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public ICollection<Requestforproceeding> Requestforproceeding { get; set; }
        public ICollection<Paymentverification> Paymentverification { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }

        //  public virtual ICollection<Approvalproccess> ApprovalproccessSendFromUser { get; set; }
    }
}
