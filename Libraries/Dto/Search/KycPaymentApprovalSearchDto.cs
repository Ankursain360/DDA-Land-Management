


using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class KycPaymentApprovalSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int StatusId { get; set; }
        public int approvalstatusId { get; set; }
        public string PlotNo { get; set; }
        public string property { get; set; }
        public string Fileno { get; set; }
        public string zone { get; set; }
        public string locality { get; set; }
    }
}

