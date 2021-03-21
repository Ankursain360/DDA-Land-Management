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
        //  [Required(ErrorMessage = " Refernce number is mandatory")]
        public int? RefId { get; set; }
        public int? PaymentTypeId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string ChallanUtrnumber { get; set; }
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
