using Dto.Common;
using System;

namespace Dto.Search
{
    public class DoortodoorsurveySearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string location { get; set; }
        public string occupantname { get; set; }
        public string Mobileno { get; set; }
        public string presentuse { get; set; }
        public string createdByNavigation { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
