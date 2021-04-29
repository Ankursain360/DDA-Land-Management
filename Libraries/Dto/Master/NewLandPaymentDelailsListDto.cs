using Dto.Common;

namespace Dto.Master
{
   public class NewLandPaymentDelailsListDto
    {
        public int Id { get; set; }
        public string DemandListNo { get; set; }
        public string EnmSno { get; set; }
        public string BankName { get; set; }
        public string VoucherNo { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string PercentPaid { get; set; }


        public string IsActive { get; set; }
    }
}
