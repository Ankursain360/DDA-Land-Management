using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
   public class Newlandkhasra : AuditableEntity<int>
    {


        [Required(ErrorMessage = "Khasra is mandatory")]
        public string Name { get; set; }
                
        public int NewLandvillageId { get; set; }
        public int LandCategoryId { get; set; }

        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Rect No is mandatory")]
        public string RectNo { get; set; }
        public byte IsActive { get; set; }




       
        [NotMapped]
        public List<LandCategory> LandCategoryList { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }

        public LandCategory LandCategory { get; set; }

        //   public virtual LandCategory LandCategory { get; set; }
        //[NotMapped]
        //public List<Newlandkhasra> KhasraList { get; set; }
        
        public Newlandvillage Newlandvillage { get; set; }
        //  public virtual Newlandvillage Newlandvillage { get; set; }




    }
}
