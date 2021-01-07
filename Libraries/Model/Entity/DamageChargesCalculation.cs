using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Libraries.Model.Entity
{
    public class DamageChargesCalculation 
    {
        public decimal? Area { get; set; }
        public string Months { get; set; }
        public decimal? DamageCharges { get; set; }
        public decimal? Compunding { get; set; }

        public int? PropertyTypeId { get; set; }
        public int? LocalityId { get; set; }
        public DateTime? EncroachmentDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Rate { get; set; }
        public decimal? TotalGrant { get; set; }
        public decimal? TotalInterest { get; set; }
        public decimal? RebateOfTotalInterest { get; set; }
        public decimal? TotalPayAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RemainAmount { get; set; }
        public int? GridId { get; set; }
    }


}
