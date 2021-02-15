using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class AppealdetailSearchDto : BaseSearchDto
    {
        public string demandListNo { get; set; }
        public string appealNo { get; set; }
        public string panelLawer { get; set; }
    }
}

