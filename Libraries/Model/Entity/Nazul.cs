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

       
        [Required(ErrorMessage = "Village is Mandatory", AllowEmptyStrings = false)]
        public int VillageId { get; set; }
        public string JaraiSakani { get; set; }
        public string Language { get; set; }

        [Required(ErrorMessage = "Date of Consolidation is Mandatory")]
        public DateTime YearOfConsolidation { get; set; }

        [Required(ErrorMessage = "Date of Jamabandi is Mandatory")]
        public DateTime YearOfJamabandi { get; set; }

        [Required(ErrorMessage = "Last Mutation No is Mandatory")]
        public string LastMutationNo { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte IsActive { get; set; }
      

        public Acquiredlandvillage Village { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
    }

}
