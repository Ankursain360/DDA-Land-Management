

using Dto.Common;
namespace Dto.Master
{
    public class RebateListDto
    {
        public int Id { get; set; }
        public string RebateOn { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RebatePercentage { get; set; }
        public string Status { get; set; }
    }
}
