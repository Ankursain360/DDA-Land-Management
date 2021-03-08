using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
        public class Undersection17plotdetail : AuditableEntity<int>
        {
        [Required(ErrorMessage = " UnderSection 17 is mandatory", AllowEmptyStrings = false)]
        public int? UnderSection17Id { get; set; }
        [Required(ErrorMessage = " Village is mandatory", AllowEmptyStrings = false)]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = " Khasra is mandatory", AllowEmptyStrings = false)]
        public int? KhasraId { get; set; }
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [Required(ErrorMessage = "Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }
            public string Remarks { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }

            public Khasra Khasra { get; set; }
            public Undersection17 UnderSection17 { get; set; }
            public Acquiredlandvillage Acquiredlandvillage { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        [NotMapped]
        public List<Undersection17> Undersection17List { get; set; }
        


    }
    }

