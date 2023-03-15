using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class EncroachmentRegisterDashboardDto: BaseSearchDto
    {
        public int TotalRequest { get; set; }
        public int AcceptAndInitiateDemolition { get; set; }
        public int TotalPending { get; set; }
        public int TotalDemolitionToBETakenLater { get; set; }
        public int PendingAtYou { get; set; }
       
    }
}
