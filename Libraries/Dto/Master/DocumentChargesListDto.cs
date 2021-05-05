using Dto.Common;
namespace Dto.Master
{
   public class DocumentChargesListDto
    {
        public int Id { get; set; }

        public string LeasePurpose { get; set; }

        public string LeaseSubPurpose { get; set; }

        public string DocumentCharges { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Status { get; set; }
    }
}
