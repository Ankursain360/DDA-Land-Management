using Dto.Common;
using System;

namespace Dto.Search
{
   public class TrackingListDataDto : AuditableDto<int>
    {

        public int? SendFrom { get; set; }
        public int? SendTo { get; set; }
        public string Remarks { get; set; }
    }
}
