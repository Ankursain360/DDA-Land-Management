using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Possessiondetails : AuditableEntity<int>
    {
       
        [Required]
        public int? VillageId { get; set; }
        [Required]
        public int? KhasraId { get; set; }
        [Required]
        public string PossType { get; set; }
        [Required]
        public string ReasonNonPoss { get; set; }
        [Required]
        public DateTime PossDate { get; set; }
        public string PlotNo { get; set; }
        [Required]
        public decimal Bigha { get; set; }
        [Required]
        public decimal Biswa { get; set; }
        public string Remarks { get; set; }
  
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public Khasra Khasra { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        public Acquiredlandvillage Village { get; set; }
    }
}
