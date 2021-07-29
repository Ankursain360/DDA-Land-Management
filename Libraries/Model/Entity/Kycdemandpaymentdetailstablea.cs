
using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Libraries.Model.Entity
{
    public class Kycdemandpaymentdetailstablea : AuditableEntity<int>
    {
        public int KycId { get; set; }
        public int DemandPaymentId { get; set; }
        public string DemandPeriod { get; set; }
        public decimal? GroundRent { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? TotdalDues { get; set; }
        public byte IsActive { get; set; }
        public Kycdemandpaymentdetails DemandPayment { get; set; }
        public Kycform Kyc { get; set; }
    }
}