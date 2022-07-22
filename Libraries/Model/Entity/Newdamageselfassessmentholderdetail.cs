using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Newdamageselfassessmentholderdetail : AuditableEntity<int>
    {
        public int NewDamageSelfAssessmentId { get; set; }
        public string NameOfGpaats { get; set; }
        public string DeathCertificateNo { get; set; }
        public DateTime? DeathCertificateDate { get; set; }
        public string NameOfSurvivingMember { get; set; }
        public string Relationship { get; set; }
        public string IsRelinquished { get; set; }

        public Newdamagepayeeregistration NewDamageSelfAssessment { get; set; }
    }
}
