using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class DemolitionDashboardDownloadDto
    {
        public int SrNo { get; set; }
        public string InspectionDate { get; set; }

        public string Department { get; set; }

        public string Zone { get; set; }
        public string KhasraNo { get; set; }
        public string DemolitionStatus { get; set; }        
        public string ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public string PendingAt { get; set; }
        
    }
}
