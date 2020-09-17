using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Khasra : AuditableEntity<int>
    {

        public string Name { get; set; }
      
        public int VillageId { get; set; }
        public int LandCategoryId { get; set; }
       
        public string Bigha { get; set; }
        public string Biswa { get; set; }
        public string Biswanshi { get; set; }
        public string Description { get; set; }
        public string RectNo { get; set; }
        public byte IsActive { get; set; }


        


        [NotMapped]
        public List<LandCategory> LandCategoryList { get; set; }
        public virtual LandCategory LandCategory { get; set; }



        [NotMapped]
        public List<Village> VillageList { get; set; }
        public virtual Village Village { get; set; }



    }
}
