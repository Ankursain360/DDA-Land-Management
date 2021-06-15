using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class LeaseApplicationFormApprovalSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int StatusId { get; set; }
        public int approvalstatusId { get; set; }
    }
}
