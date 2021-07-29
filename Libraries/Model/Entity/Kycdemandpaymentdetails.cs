using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
      public class Kycdemandpaymentdetails : AuditableEntity<int>
      {
        public Kycdemandpaymentdetails()
        {
            Kycdemandpaymentdetailstablea = new HashSet<Kycdemandpaymentdetailstablea>();
            kycdemandpaymentdetailstableb = new HashSet<Kycdemandpaymentdetailstableb>();
            kycdemandpaymentdetailstablec = new HashSet<Kycdemandpaymentdetailstablec>();
        }

        public int KycId { get; set; }
        public string IsPaymentAgreed { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal TotalPayableInterest { get; set; }
        public decimal TotalDues { get; set; }
        public byte IsActive { get; set; }

        #region Approval Related Fields
        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public int ApprovalStatusCode { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }

        [NotMapped]
        public IFormFile ApprovalDocument { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalStatusList { get; set; }

        [NotMapped]
        public int ApprovalUserId { get; set; }

        [NotMapped]
        public int ApprovalRoleId { get; set; }

        #endregion

        public Approvalstatus ApprovedStatusNavigation { get; set; }
        public Kycform Kyc { get; set; }
        public ICollection<Kycdemandpaymentdetailstablea> Kycdemandpaymentdetailstablea { get; set; }
        public ICollection<Kycdemandpaymentdetailstableb> kycdemandpaymentdetailstableb { get; set; }
        public ICollection<Kycdemandpaymentdetailstablec> kycdemandpaymentdetailstablec { get; set; }

    }
}
