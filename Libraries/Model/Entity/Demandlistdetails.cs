using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Demandlistdetails : AuditableEntity<int>
    {
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
        public int VillageId { get; set; }
        public int? KhasraNoId { get; set; }
        public string AwardNo { get; set; }
        public string PartyName { get; set; }
        public decimal? EnhancedRatePerBigha { get; set; }
        public decimal? ExistingRatePerBigha { get; set; }
        public string CourtInvolves { get; set; }
        public decimal? PayableAmt { get; set; }
        public decimal? ApealableAmt { get; set; }
        public DateTime? JundgementDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ReasonForNonPay { get; set; }
        public string Remarks { get; set; }
        public Khasra KhasraNo { get; set; }
        public Acquiredlandvillage Village { get; set; }
    }
}
