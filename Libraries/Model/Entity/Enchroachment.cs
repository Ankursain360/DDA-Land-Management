using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public  class Enchroachment : AuditableEntity<int>
    {
      
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public string LandUse { get; set; }
        public DateTime? DateofDetection { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public int NatureofencroachmentId { get; set; } 
        public string FileNo { get; set; }
        public DateTime? ActionDate { get; set; }
        public int ReasonsId { get; set; }
        public string DamageArea { get; set; }
        public string ActionRemarks { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public string PaymentAddress { get; set; }
        public byte IsActive { get; set; }
        public string RecStatus { get; set; } 
        public string ActionTaken { get; set; }

        [NotMapped]
        public List<Natureofencroachment> NencroachmentList { get; set; }
        public virtual Natureofencroachment Natureofencroachment { get; set; }


        [NotMapped]
        public List<Reasons> ReasonsList { get; set; }
        public virtual Reasons Reasons { get; set; }





        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }

    }
}
