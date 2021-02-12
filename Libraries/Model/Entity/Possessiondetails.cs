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
      
        public int? VillageId { get; set; }
        public int? KhasraId { get; set; }
        public string PossType { get; set; }
        public string ReasonNonPoss { get; set; }
        public DateTime PossDate { get; set; }
        public string PlotNo { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public string Remarks { get; set; }
  
        public byte IsActive { get; set; }

        public Khasra Khasra { get; set; }
        public Acquiredlandvillage Village { get; set; }
    }
}
