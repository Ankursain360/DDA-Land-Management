using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class OnlinecomplaintApprovalSearchDto : BaseSearchDto
    {
     
        public int StatusId { get; set; }
        public string name { get; set; }
        public int approvalstatusId { get; set; }
    }
}
