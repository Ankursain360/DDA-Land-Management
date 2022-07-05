using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class NewdamageAddfloor : AuditableEntity<int>
    {
        //public string RegId { get; set; }
        //public string FloorName { get; set; }
        //public decimal? CarpetArea { get; set; }
        //public string ElectricityNumber { get; set; }
        //public string MuncipleTaxId { get; set; }
        //public string WaterBill { get; set; }
        //public string CurrentUse { get; set; }
        //public string Status { get; set; }
        //public int NewDamageSelfAssessmentId { get; set; }

        public int NewDamageSelfAssessmentId { get; set; }
        public string FloorName { get; set; }
        public decimal? CarpetArea { get; set; }
        public string ElectricityNumber { get; set; }
        public string MuncipleTaxId { get; set; }
        public string WaterBill { get; set; }
        public string CurrentUse { get; set; }
        public string Status { get; set; }

        //[NotMapped]
        //public List<NewDamageSelfAssessment> DamageSelfAssessmentsList { get; set; } 
        public NewDamageSelfAssessment GetDamageSelfAssessment { get; set; }
        
    }
}
