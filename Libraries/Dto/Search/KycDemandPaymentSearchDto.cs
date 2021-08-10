using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class KycDemandPaymentSearchDto : BaseSearchDto
    {
        public int KycDemandPaymentPending { get; set; }
        public int  KycDemandPaymentApprove { get; set; }
        public int KycDemandPaymentInProcess { get; set; }

        public int KycDemandPaymentInDeficiency { get; set; }

        public int KycDemandPaymentRejected { get; set; }
    }
}
