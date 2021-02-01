using Dto.Common;
namespace Dto.Search
{
   public class EnchroachmentSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int departmentId { get; set; }
        public int zoneId { get; set; }
        public int divisionId { get; set; }
        public int localityId { get; set; }

       
    }
}
