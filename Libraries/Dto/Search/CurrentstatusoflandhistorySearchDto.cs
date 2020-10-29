using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class CurrentstatusoflandhistorySearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int landtransferId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}
