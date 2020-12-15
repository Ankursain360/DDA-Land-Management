using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Damagepaymenthistory : AuditableEntity<int>
    {
       
        public int DamagePayeeRegisterId { get; set; }
        public string Name { get; set; }
        public string RecieptNo { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string RecieptDocumentPath { get; set; }
       
        public byte? IsActive { get; set; }
       
        public Damagepayeeregister DamagePayeeRegister { get; set; }
        [NotMapped]
        public List<IFormFile> RecieptDocument { get; set; }
    }
}
