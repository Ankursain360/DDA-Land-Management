using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Acquiredenhancecompensation : AuditableEntity<int>
    {
        public Acquiredenhancecompensation()
        {
            Appealdetail = new HashSet<Appealdetail>();
            Paymentdetail = new HashSet<Paymentdetail>();
            Demandlistdetails = new HashSet<Demandlistdetails>();
        }
        public string ApDemandListNo { get; set; }
        public string ApEnmSNo { get; set; }
        //public object ApEnmSNo { get; set; }
        public string AppealNo { get; set; }
        public string AppealByDept { get; set; }
        public DateTime? DateOfAppeal { get; set; }
        public string PanelLawer { get; set; }
        public string Department { get; set; }
        public string PayDemandListNo { get; set; }
        public string PayEnmSNo { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string VoucherNo { get; set; }
        public decimal PercentPaid { get; set; }
        public string PaymentProofDocumentName { get; set; }
        public string DemandListNo { get; set; }
        public int? ENMSNo { get; set; }
        public string LACFileNo { get; set; }
        public DateTime? LBRefDate { get; set; }
        public string LBNo { get; set; }
        public string RFANo { get; set; }
        public string LACNo { get; set; }
        public string SLPNo { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string DDAFileNo { get; set; }
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
        public string ENMDocumentName { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }

        public Khasra KhasraNo { get; set; }
        public Acquiredlandvillage Village { get; set; }
        public ICollection<Appealdetail> Appealdetail { get; set; }
        public ICollection<Paymentdetail> Paymentdetail { get; set; }
        public ICollection<Demandlistdetails> Demandlistdetails { get; set; }
    }
}