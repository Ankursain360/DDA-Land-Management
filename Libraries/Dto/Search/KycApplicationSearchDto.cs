using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class KycApplicationSearchDto : BaseSearchDto
    {
        public int KycApplicaionPending { get; set; }
        public int KycApplicaionApprove { get; set; }
        public int KycApplicaionInProcess { get; set; }

        public int KycApplicationDeficiency { get; set; }

        public int KycApplicationInRejected { get; set; }

        public int KycApplicationInTotal { get; set; }
    }
}
