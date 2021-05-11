


using Dto.Common;
namespace Dto.Master
{
    public class RateListDto
    {
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RatePercentage { get; set; }
        public string Status { get; set; }
    }
}
