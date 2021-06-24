using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
   public class Leasepaymentdetails : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Reference Number is mandatory")]
        public int? RefId { get; set; }
        [Required(ErrorMessage = "Payment Type is mandatory")]
        public int? PaymentTypeId { get; set; }

        public string PaymentMode { get; set; }
        [Required(ErrorMessage = "Payment Date is mandatory")]
        public DateTime? PaymentDate { get; set; }
        [Required(ErrorMessage = "Challan/UTR number is mandatory")]
        public string ChallanUtrnumber { get; set; }
        [Required(ErrorMessage = "Payment Amount is mandatory")]
        public decimal? PaymentAmount { get; set; }
        public byte? IsActive { get; set; }
        
        public Leasepaymenttype PaymentType { get; set; }
        public Allotmententry Ref { get; set; }

              
        [NotMapped]
        public List<Allotmententry> AllotmententryList { get; set; }
        [NotMapped]
        public List<Leasepaymenttype> PaymenttypeList { get; set; }
    }
}
