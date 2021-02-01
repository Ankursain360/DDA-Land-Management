using Dto.Common;
using System;

namespace Dto.Search
{
    public class InspectionEncroachmentregistrationSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int departmentId { get; set; }
        public int zoneId { get; set; }
        public int divisionId { get; set; }
        public int localityId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
