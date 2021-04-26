using Dto.Common;
using System;

namespace Dto.Search
{
    public class ApprovalHistoryListDataDto
    {
        public int Id { get; set; }
        public string ProcessGuid { get; set; }
        public int ServiceId { get; set; }
        public string SendFrom { get; set; }
        public string SendTo { get; set; }
        public DateTime SubmitDate { get; set; }
        public string ActionTakenDate { get; set; }
        public string DocumentName { get; set; }
        public string SentStatusName { get; set; }
        public string Remarks { get; set; }
    }
}