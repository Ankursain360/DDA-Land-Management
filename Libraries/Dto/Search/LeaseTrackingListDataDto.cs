using Dto.Common;
using System;
namespace Dto.Search
{
   public class LeaseTrackingListDataDto : AuditableDto<int>
    {
        public string Remarks { get; set; }
        public string From_User { get; set; }
        public string To_User { get; set; }
        public DateTime Send_Date { get; set; }
    }
}
