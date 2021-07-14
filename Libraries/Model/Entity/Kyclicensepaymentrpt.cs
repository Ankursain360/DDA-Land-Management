using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Kyclicensepaymentrpt : AuditableEntity<int>
    {
       
        public int KycformId { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string PaymentPeriod { get; set; }
        public string Purpose { get; set; }
        public string PaymentDocPath { get; set; }
        public byte IsActive { get; set; }
       

        public Kycform Kycform { get; set; }
    }
}
