
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
        [Required(ErrorMessage = " Payment Type is mandatory")]
        public string PaymentType { get; set; }
        public string Period { get; set; }
        [Required(ErrorMessage = " Challan No is mandatory")]
        public string ChallanNo { get; set; }
        [Required(ErrorMessage = " Amount is mandatory")]
        public decimal? Amount { get; set; }
        [Required(ErrorMessage = " Date of Payment is mandatory")]
        public DateTime? DateofPaymentByAllottee { get; set; }
        [Required(ErrorMessage = " Proof of payment is mandatory")]
        public string Proofinpdf { get; set; }
        public string Ddabankcredit { get; set; }
        public byte IsActive { get; set; }
    
        public Kycdemandpaymentdetails DemandPayment { get; set; }

       

        [NotMapped]
        public string UploadFilePath { get; set; }
        public Kycform Kyc { get; set; }
    }
}