using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
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
        public Demandlistdetails()
        {
            Appealdetail = new HashSet<Appealdetail>();
            Paymentdetail = new HashSet<Paymentdetail>();
        }
        [Required(ErrorMessage = "Demand List No. is Mandatory ")]
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
        public string ENMDocumentName { get; set; }
        public string Remarks { get; set; }
        public Khasra KhasraNo { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }
        public Acquiredlandvillage Village { get; set; }

        //******Appeal****//
        [NotMapped]
        public string AppealNo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "AppealByDept name is Mandatory")]
        public string AppealByDept { get; set; }
        [NotMapped]

        public string Department { get; set; }
        //public int? DemandListId { get; set; }
        [NotMapped]
        public DateTime? DateOfAppeal { get; set; }
        [NotMapped]

        public string PanelLawer { get; set; }
        //****Payment******//
        [NotMapped]
        public decimal? AmountPaid { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "ChequeDate  is Mandatory")]

        public DateTime? ChequeDate { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "ChequeNo  is Mandatory")]
       
        public string ChequeNo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "BankName is Mandatory")]
        public string BankName { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "VoucherNo is Mandatory")]
        public string VoucherNo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Percent Paid is Mandatory")]
        public decimal? PercentPaid { get; set; }
        [NotMapped]
        public string PaymentProofDocumentName { get; set; }
      
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
     
        [NotMapped]
        public List<Khasra> KhasraNoList { get; set; }
        [NotMapped]
        public IFormFile PaymentProofDocumentIFormFile { get; set; }
      
        [NotMapped]
        public IFormFile ENMDocumentIFormFile { get; set; }
       

       
        public ICollection<Appealdetail> Appealdetail { get; set; }
        public ICollection<Paymentdetail> Paymentdetail { get; set; }
    }
}
