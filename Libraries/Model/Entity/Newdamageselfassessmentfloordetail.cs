using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Newdamageselfassessmentfloordetail : AuditableEntity<int>
    {
        public int NewDamageSelfAssessmentId { get; set; }
        public int? FloorId { get; set; }
        public decimal? CarpetArea { get; set; }
        public string ElectricityKno { get; set; }
        public string McdpropertyTaxId { get; set; }
        public string WaterKno { get; set; }
        public string CurrentUse { get; set; }

        public Floors Floor { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public byte IsActive { get; set; }

        [NotMapped]
        public List<Floors> FloorList { get; set; }
        public Newdamagepayeeregistration NewDamageSelfAssessment { get; set; }
    }
}
