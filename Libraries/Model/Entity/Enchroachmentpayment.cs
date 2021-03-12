using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
   public class Enchroachmentpayment    : AuditableEntity<int>
    {

        [Required(ErrorMessage = " Encroacher Name is mandatory")]
        [Remote(action: "Exist", controller: "Actions", AdditionalFields = "Id")]
        public int EnchId { get; set; }
        [Required(ErrorMessage = " Amount is mandatory")]
        public string Amount { get; set; }
        [Required(ErrorMessage = " Cheque Number is mandatory")]
        public string ChequeNo { get; set; }
        [Required(ErrorMessage = " Cheque Date is mandatory")]
        public string ChequeDate { get; set; }
        public byte RecState { get; set; }
        
        //        public ICollection<Enchroachment> Enchroachments { get; set; }
        
        public byte? IsActive { get; set; }

        public Enchroachment Enchroachment { get; set; }
    }
}
