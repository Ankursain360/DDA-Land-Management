using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Demandletter : AuditableEntity<int>
    {

      
        public int FileNo { get; set; }
        public int? DemandPeriod { get; set; }
        public int? DemandNumber { get; set; }
        public DateTime? DemandDate { get; set; }
        public decimal? PreviousBalanceAmount { get; set; }
        public decimal? OutStandingAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string DemandLetterFilePath { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Demandletter> Damagelist { get; set; }



    }
}
