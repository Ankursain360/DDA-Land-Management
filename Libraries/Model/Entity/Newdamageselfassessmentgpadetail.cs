using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Newdamageselfassessmentgpadetail : AuditableEntity<int>
    {
        public int NewDamageSelfAssessmentId { get; set; }
        public DateTime? DateOfExecutionOfGpa { get; set; }
        public string NameOfTheSeller { get; set; }
        public string NameOfThePayer { get; set; }
        public string AddressOfThePlotAsPerGpa { get; set; }
        public string AreaOfThePlotAsPerGpa { get; set; }
        public string GpafilePath { get; set; }

        public Newdamagepayeeregistration NewDamageSelfAssessment { get; set; }
    }
}
