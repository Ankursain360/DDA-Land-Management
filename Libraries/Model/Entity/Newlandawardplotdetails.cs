using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandawardplotdetails : AuditableEntity<int>
    {
        public Newlandawardplotdetails()
        {
          //  Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();
        }

        [Required(ErrorMessage = "Award  is mandatory")]
        public int AwardMasterId { get; set; }
        [Required(ErrorMessage = "Village  is mandatory")]
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }

        public virtual Newlandawardmasterdetail NewlandAwardMaster { get; set; }
        [NotMapped]
        public List<Newlandawardmasterdetail> NewlandAwardmasterList { get; set; }

        [NotMapped]
        public List<Newlandkhasra> NewlandKhasraList { get; set; }
        public virtual Newlandkhasra NewlandKhasra { get; set; }

        [NotMapped]
        public List<Newlandvillage> NewlandVillageList { get; set; }
        public virtual Newlandvillage NewlandVillage { get; set; }

       
    }
}
