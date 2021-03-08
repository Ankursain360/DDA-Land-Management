using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Demandlistdetails : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Demand List No. is Mandatory Field")]
        public string DemandListNo { get; set; }
        public int? Enmsno { get; set; }
        public string LacfileNo { get; set; }
        public DateTime? LbrefDate { get; set; }
        public string Lbno { get; set; }
        public string Rfano { get; set; }
        public string Lacno { get; set; }
        public string Slpno { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string DdafileNo { get; set; }
        public string BalanceInterestCase { get; set; }
        public string PayableAppealable { get; set; }
        public DateTime? AwardDate { get; set; }

        [Required(ErrorMessage = "Village is Mandatory", AllowEmptyStrings = false)]
        public int VillageId { get; set; }

        [Required(ErrorMessage = "Khasra is Mandatory", AllowEmptyStrings = false)]
        public int? KhasraNoId { get; set; }
        public string AwardNo { get; set; }
        public string PartyName { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Enhanced Rate; Max 18 digits")]
        public decimal? EnhancedRatePerBigha { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Existing Rate ; Max 18 digits")]
        public decimal? ExistingRatePerBigha { get; set; }
        public string CourtInvolves { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Payable Amount; Max 18 digits")]
        public decimal? PayableAmt { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Apealable Amount; Max 18 digits")]
        public decimal? ApealableAmt { get; set; }
        public DateTime? JundgementDate { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Amount; Max 18 digits")]
        public decimal? TotalAmount { get; set; }
        public string ReasonForNonPay { get; set; }
        public string Remarks { get; set; }
        public Khasra KhasraNo { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }
        public Acquiredlandvillage Village { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Khasra> KhasraNoList { get; set; }
    }
}
