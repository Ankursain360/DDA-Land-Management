using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Newlandpaymentdetail : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Demand List No is mandatory")]
        public string DemandListNo { get; set; }
        [Required(ErrorMessage = "Enm  SNo is mandatory")]
        public string EnmSno { get; set; }
        [Required(ErrorMessage = "Amound Paid is mandatory")]
        public decimal AmountPaid { get; set; }
        [Required(ErrorMessage = "Cheque Date is mandatory")]
        public DateTime? ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string VoucherNo { get; set; }
        [Required(ErrorMessage = "Percent Paid is mandatory")]
        public decimal PercentPaid { get; set; }
        public string PaymentProofDocumentName { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }

        [NotMapped]
        public IFormFile PaymentProofDocumentIFormFile { get; set; }

    }
}
