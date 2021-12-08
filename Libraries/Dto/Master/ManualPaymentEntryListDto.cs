using Dto.Common;

namespace Dto.Master
{
   public class ManualPaymentEntryListDto
    {
        public int Id { get; set; }
        public string FleNo { get; set; }
        public string PayeeName { get; set; }
        public string BankName { get; set; }
        public string PaymentMode { get; set; }
        public string DDNo { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public string PropertyNo { get; set; }
    }
}
