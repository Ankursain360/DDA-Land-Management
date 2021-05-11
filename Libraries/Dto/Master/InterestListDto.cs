


using Dto.Common;
namespace Dto.Master
{
    public class InterestListDto
    {  
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string InterestPercentage { get; set; }
        public string Status { get; set; }
    }
}
