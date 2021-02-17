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
        public string Amount { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public byte RecState { get; set; }
        public byte IsActive { get; set; }
        //        public ICollection<Enchroachment> Enchroachments { get; set; }
    }
}
