using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Awardplotdetails : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Award  is mandatory")]
        public int AwardMasterId { get; set; }
        [Required(ErrorMessage = "Village  is mandatory")]
        public int VillageId { get; set; }
        [Required(ErrorMessage = "Khasra is mandatory")]
        public int KhasraId { get; set; }
        [Required(ErrorMessage = "Bigha  is mandatory")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi  is mandatory")]
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }

        public virtual Awardmasterdetail AwardMaster{ get; set; }
        [NotMapped]
        public List<Awardmasterdetail> AwardmasterList { get; set; }

        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }




        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public virtual Acquiredlandvillage Village { get; set; }



    }
}
