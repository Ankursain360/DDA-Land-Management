using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Newdamagepaymenthistory : AuditableEntity<int>
    {
        public int NewDamageSelfAssessmentId { get; set; }
        public string Name { get; set; }
        public string RecieptNo { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string RecieptDocumentPath { get; set; }
        public byte? IsActive { get; set; }

        public Newdamagepayeeregistration NewDamageSelfAssessment { get; set; }
    }
}
