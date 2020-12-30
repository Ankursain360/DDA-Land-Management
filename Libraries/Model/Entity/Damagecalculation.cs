using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Libraries.Model.Entity
{
    public class Damagecalculation : AuditableEntity<int>
    {
        public decimal? Area { get; set; }
        public int? Months { get; set; }
        public decimal? DamageCharges { get; set; }
        public decimal? Compunding { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalGrant { get; set; }
        public decimal? TotalInterest { get; set; }
        public decimal? RebateOfTotalInterest { get; set; }
        public decimal? TotalPayAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RemainAmount { get; set; }
        public int? GridId { get; set; }
        public byte? IsActive { get; set; }

        public PropertyType PropertyType { get; set; }
        public int? PropertyTypeId { get; set; }
        [NotMapped]
        public List<PropertyType> PropertyType1 { get; set; }
        [NotMapped]
        public List<Damagecalculation> DamageRateCalculationList { get; set; }
        [NotMapped]
        public List<Damagecalculation> DamageIntrestCalculationList { get; set; }
        public int? LocalityId { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }

    }


}
