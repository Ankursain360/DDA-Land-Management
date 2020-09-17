using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Jointsurvey : AuditableEntity<int>
    {
       
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public string SitePosition { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string NatureOfStructure { get; set; }
        public DateTime? JointSurveyDate { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }



    }
}
