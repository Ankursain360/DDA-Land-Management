using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;
namespace Dto.Search
{
   public class KycFormApprovalDetailsSearchDto :BaseSearchDto
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

        public string approvalNo { get; set; }

        public string approvalType { get; set; }
        
        
    }
}
