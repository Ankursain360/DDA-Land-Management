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
        public int LandUseId { get; set; }
        public DateTime? DateofDetection { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public int NEncroachmentId { get; set; } 
        public string FileNo { get; set; }
        public DateTime? ActionDate { get; set; }
        public int ReasonId { get; set; }
        public string DamageArea { get; set; }
        public string ActionRemarks { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public string PaymentAddress { get; set; }
        public byte IsActive { get; set; }


        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }

    }
}
