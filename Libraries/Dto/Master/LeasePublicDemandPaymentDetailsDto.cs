using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dto.Master
{
   public class LeasePublicDemandPaymentDetailsDto
    {
        public int Id { get; set; }

        public string FileNo { get; set; }
        public int KycId { get; set; }
        public string IsPaymentAgreed { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal TotalPayableInterest { get; set; }
        public decimal TotalDues { get; set; }
        public byte IsActive { get; set; }
        public string ChallanNo { get; set; }
        public int DemandPaymentId { get; set; }
        public string DemandPeriod { get; set; }
        public decimal? GroundRent { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? TotdalDues { get; set; }

        public string ChallanNoR { get; set; }
        public string ChallanAmount { get; set; }

        [NotMapped]
        public List<string> PaymentType { get; set; }

        [NotMapped]
        public List<string> Period { get; set; }

        [NotMapped]
        public List<string> ChallanNoForPayment { get; set; }
        [NotMapped]

        public List<Decimal> Amount { get; set; }
        [NotMapped]

        public List<DateTime> DateofPaymentByAllottee { get; set; }
        [NotMapped]
        public List<string> Proofinpdf { get; set; }
        [NotMapped]
        public List<string> Ddabankcredit { get; set; }






    }
}
