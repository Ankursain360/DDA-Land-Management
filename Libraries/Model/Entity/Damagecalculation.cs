using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Libraries.Model.Entity
{
    public class Damagecalculation : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Area is Mandatory Field")]
        public decimal? Area { get; set; }
        public int? Months { get; set; }
        public decimal? DamageCharges { get; set; }
        public decimal? Compunding { get; set; }

        [Required(ErrorMessage = "Property Type is Mandatory Field")]
        public int? PropertyTypeId { get; set; }

        [Required(ErrorMessage = "Locality is Mandatory Field")]
        public int? LocalityId { get; set; }

        [Required(ErrorMessage = "Start Date is Mandatory Field")]
        public DateTime? EncroachmentDate { get; set; }

        [Required(ErrorMessage = "Start Date is Mandatory Field")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is Mandatory Field")]
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
        public Locality Locality { get; set; }
        public PropertyType PropertyType { get; set; }
        [NotMapped]
        public List<PropertyType> PropertyType1 { get; set; }
        [NotMapped]
        public List<Damagecalculation> DamageRateCalculationList { get; set; }
        [NotMapped]
        public List<Damagecalculation> DamageIntrestCalculationList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }

        /*DamageCalculation Grid*/
        [NotMapped]
        public DateTime StartDateCal { get; set; }
        [NotMapped]
        public DateTime EndDateCal { get; set; }
        [NotMapped]
        public decimal RateCal { get; set; }
        [NotMapped]
        public decimal AreaCal { get; set; }
        [NotMapped]
        public int MonthCal { get; set; }
        [NotMapped]
        public decimal DamageChargesCal { get; set; }

    }


}
