using Dto.Common;
using System;

namespace Dto.Search
{
 public   class Unotification6detailsListDto : AuditableDto<int>
    {

        public string UnderSection6No { get; set; }
        public string VillageName { get; set; }
        public string KhasraName { get; set; }
        public Int32 BighaArea { get; set; }
        public Int32 BiswaArea { get; set; }
        public Int32 BiswaniArea { get; set; }
        public Int32 NotifyBighaArea { get; set; }
        public Int32 NotifyBiswaArea { get; set; }

    }
}
