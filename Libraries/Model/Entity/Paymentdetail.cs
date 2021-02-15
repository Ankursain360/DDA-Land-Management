using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Paymentdetail : AuditableEntity<int>
    {
        [Required(ErrorMessage = "DemandListNo is Mandatory")]
        public string DemandListNo { get; set; }
        [Required(ErrorMessage = "EnmSno  is Mandatory")]
        public string EnmSno { get; set; }
        [Required(ErrorMessage = "AmountPaid is Mandatory")]
        public decimal AmountPaid { get; set; }
        [Required(ErrorMessage = "ChequeDate  is Mandatory")]
        public DateTime? ChequeDate { get; set; }
        [Required(ErrorMessage = "ChequeNo  is Mandatory")]
        public string ChequeNo { get; set; }
        [Required(ErrorMessage = "BankName is Mandatory")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "VoucherNo is Mandatory")]
        public string VoucherNo { get; set; }
        public decimal PercentPaid { get; set; }
        public byte IsActive { get; set; }
       
    }
}
