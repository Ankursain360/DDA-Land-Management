using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Nazul : AuditableEntity<int>
    {

        //public int VillageId { get; set; }
        //public string JaraiSakani { get; set; }
        //public string Language { get; set; }
        //public DateTime YearOfConsolidation { get; set; }
        //public DateTime YearOfJamabandi { get; set; }
        //public string LastMutationNo { get; set; }
        //public byte IsActive { get; set; }


        //public Acquiredlandvillage Acquiredlandvillage { get; set; }
        //[NotMapped]
        //public List<Acquiredlandvillage> VillageList { get; set; }
        [Required(ErrorMessage = "Village is Mandatory Field", AllowEmptyStrings = false)]
        public int VillageId { get; set; }
        public string JaraiSakani { get; set; }
        public string Language { get; set; }
        public DateTime YearOfConsolidation { get; set; }
        public DateTime YearOfJamabandi { get; set; }
        [Required(ErrorMessage = "LastMutationNo is Mandatory Field")]
        public string LastMutationNo { get; set; }
        public byte IsActive { get; set; }
      

        public Acquiredlandvillage Village { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
    }

}
