using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Newdamageselfassessmentatsdetail : AuditableEntity<int>
    {
        public int NewDamageSelfAssessmentId { get; set; }
        public DateTime? DateOfExecutionOfAts { get; set; }
        public string NameOfTheSellerAts { get; set; }
        public string NameOfThePayerAts { get; set; }
        public string AddressOfThePlotAsPerAts { get; set; }
        public string AreaOfThePlotAsPerAts { get; set; }
        public string AtsfilePath { get; set; }

        public Newdamagepayeeregistration NewDamageSelfAssessment { get; set; }
    }
}
