using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class LegalReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int? FileNo { get; set; }
        public int? CaseNo { get; set; }
        public int? ContemptOfCourt { get; set; }
        public int? CourtType { get; set; }
        public int? CaseStatus { get; set; }
        public int? Zone { get; set; }
        public int? Locality { get; set; }
        public int? StayInterimGranted { get; set; }
        public int? Judgement { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
