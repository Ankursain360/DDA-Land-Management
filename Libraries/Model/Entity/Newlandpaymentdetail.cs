using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Newlandpaymentdetail : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Demand List No is mandatory feild")]
        public string DemandListNo { get; set; }
        [Required(ErrorMessage = "Enm  SNo is mandatory feild")]
        public string EnmSno { get; set; }
        [Required(ErrorMessage = "Enm  SNo is mandatory feild")]
        public decimal AmountPaid { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string VoucherNo { get; set; }
        public decimal PercentPaid { get; set; }
        public byte IsActive { get; set; }
       
    }
}
