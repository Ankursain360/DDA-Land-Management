using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public class Enhancecompensation : AuditableEntity<int>
    {
      
        public string DemandListNo { get; set; }
        public string Enmsno { get; set; }
        public string LacfileNo { get; set; }
        public string Lbno { get; set; }
        public DateTime? LbrefDate { get; set; }
        public string DdafileNo { get; set; }
        public string Lacno { get; set; }
        public string Rfano { get; set; }
        public string Slpno { get; set; }
        public string Payable { get; set; }
        public DateTime? JudgementDate { get; set; }
        public string CaseCourt { get; set; }
        public string PartyName { get; set; }
        public byte DemandStatus { get; set; }
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string AmountPaid { get; set; }
        public DateTime? CheckDate { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string VoucherNo { get; set; }
        public string PercentPaid { get; set; }
        public byte PaymentStatus { get; set; }
        public string AppealNo { get; set; }
        public string AppealDept { get; set; }
        public DateTime? DateOfAppeal { get; set; }
        public string PanelLawer { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
       
    }
}
