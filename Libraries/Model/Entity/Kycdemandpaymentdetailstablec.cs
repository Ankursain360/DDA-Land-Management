
using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Libraries.Model.Entity
{
    public class Kycdemandpaymentdetailstablec : AuditableEntity<int>
    {

     
        public int KycId { get; set; }
        public int DemandPaymentId { get; set; }
        public string IsVerified { get; set; }
        public string PaymentType { get; set; }
        public string Period { get; set; }
        public string ChallanNo { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateofPaymentByAllottee { get; set; }
        public string Proofinpdf { get; set; }
        public string Ddabankcredit { get; set; }
        public byte IsActive { get; set; }
    
        public Kycdemandpaymentdetails DemandPayment { get; set; }

       

        [NotMapped]
        public string UploadFilePath { get; set; }
        public Kycform Kyc { get; set; }
    }
}