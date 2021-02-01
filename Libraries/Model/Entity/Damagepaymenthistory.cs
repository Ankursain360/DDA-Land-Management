using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Damagepaymenthistory : AuditableEntity<int>
    {
       
        public int DamagePayeeRegisterTempId { get; set; }
        public string Name { get; set; }
        public string RecieptNo { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AutoCalculateAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string RecieptDocumentPath { get; set; }
      
        public byte? IsActive { get; set; }

        public Damagepayeeregister DamagePayeeRegister { get; set; }
    }
}
