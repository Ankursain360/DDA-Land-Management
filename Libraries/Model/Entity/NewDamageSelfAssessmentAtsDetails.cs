﻿using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class NewDamageSelfAssessmentAtsDetails : AuditableEntity<int>
    {
         

        public int NewDamageSelfAssessmentId { get; set; }
        public DateTime? DateOfExecutionOfAts { get; set; }
        public string NameOfTheSellerAts { get; set; }
        public string NameOfThePayerAts { get; set; }
        public string AddressOfThePlotAsPerAts { get; set; }
        public string AreaOfThePlotAsPerAts { get; set; }
        public NewDamageSelfAssessment GetSelfAssessment { get; set; }


    }
}
