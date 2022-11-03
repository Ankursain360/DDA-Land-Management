using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class DefaulterListingReportListDto
    {
        public string Locality { get; set; }
        public string FileNo { get; set; }
        public string DemandNo { get; set; }
        public string DueAmount { get; set; }
        public DateTime PaymentDueDate { get; set; }
    }
} 
