using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class KycFormDemandPaymentApprovalSearchDto : BaseSearchDto
    {

        public int Id { get; set; }
        public string Property { get; set; }

        public string FileNo { get; set; }
        public string LeaseGroundRentDepositFrequency { get; set; }
        public string PlotNo { get; set; }
        public string Phase { get; set; }
        public string Sector { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }

        public Decimal TotalPayable { get; set; }
        public Decimal TotalPayableInterest { get; set; }
        public Decimal TotalDues { get; set; }

    }
}
