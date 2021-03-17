using Dto.Common;
using System;

namespace Dto.Search
{
   public class ApprovalCompleteListDataDto : AuditableDto<int>
    {
        public Int64 User_Id { get; set; }
        public Int64 TotalPendingReq { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public int workflowTemplateId { get; set; }

    }
}
