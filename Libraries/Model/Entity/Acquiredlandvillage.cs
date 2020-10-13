using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Acquiredlandvillage : AuditableEntity<int>
    {
     
        public string Name { get; set; }
        public string Code { get; set; }
        public int TehsilId { get; set; }
        public int DistrictId { get; set; }
        public string YearofConsolidation { get; set; }
        public string TotalNoOfSheet { get; set; }
        public string Zone { get; set; }
        public string Acquired { get; set; }
        public string Circle { get; set; }
        public string WorkingVillage { get; set; }
        public int VillageTypeId { get; set; }
        public byte IsActive { get; set; }

      
        [NotMapped]
        public List<District> DistrictList { get; set; }
        public virtual District District { get; set; }

       
        [NotMapped]
        public List<Tehsil> TehsilList { get; set; }
        public virtual Tehsil Tehsil { get; set; }


     
        [NotMapped]
        public List<Villagetype> VillagetypeList { get; set; }
        public virtual Villagetype VillageType { get; set; }



    }
}
