using Dto.Common;
using System;

namespace Dto.Search
{
 public   class Unotification6detailsListDto : AuditableDto<int>
    {

        public string UnderSection6No { get; set; }
        public string VillageName { get; set; }
        public string KhasraName { get; set; }
        public decimal BighaArea { get; set; }
        public decimal BiswaArea { get; set; }
        public decimal BiswaniArea { get; set; }
        public decimal NotifyBighaArea { get; set; }
        public decimal NotifyBiswaArea { get; set; }

    }
}
