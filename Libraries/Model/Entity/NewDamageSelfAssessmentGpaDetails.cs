using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class NewDamageSelfAssessmentGpaDetails : AuditableEntity<int>
    {
        
        public int NewDamageSelfAssessmentId { get; set; }
        public DateTime? DateOfExecutionOfGpa { get; set; }
        public string NameOfTheSeller { get; set; }
        public string NameOfThePayer { get; set; }
        public string AddressOfThePlotAsPerGpa { get; set; }
        public string AreaOfThePlotAsPerGpa { get; set; }
         
        public NewDamageSelfAssessment GetNewDamageSelfAssessment { get; set; } 

    } 
}
