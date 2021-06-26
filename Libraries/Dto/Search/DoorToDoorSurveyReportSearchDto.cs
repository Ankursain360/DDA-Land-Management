using System;
using System.Collections.Generic;
using System.Text;
using Dto.Common;
namespace Dto.Search
{
   public class DoorToDoorSurveyReportSearchDto : BaseSearchDto
    {
        public int Presentuse { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
