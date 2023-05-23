using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class VillageAndKhasraBiseReport_ApiDto
    {
        public string VillageName { get; set; }
        public string Khasra_No { get; set; }
        public string AreaBhigha_Biswa_Biswana { get; set; }
        public string Notification_s_US_4 { get; set; }
        public string um4Date { get;set; }
        public string un4document { get; set; } 
        public string Notification_s_US_6 { get; set; }
        public string um6Date { get;set; }
        public string un6document { get; set; }
        public string Notification_s_US_17 { get; set; }
        public string um17Date { get;set; }
        public string un17document { get;set; }
        public string Notification_s_US_22 { get; set; }
        public string un22Date { get;set; }
        public string un22document { get;set; }
        public string AwardsNumber { get; set; } 
        public string AwardDate { get; set; }
        public string Awarddocument { get; set; }
        public string DateofPossesion { get; set; }
        public string possessiondocument { get; set; }

        public class VillageAndKhasraBiseReportResponseDetails 
        {
            public string responseCode { get; set; }
            public string responseMessage { get; set; }
            public List<VillageAndKhasraBiseReport_ApiDto> response { get; set; }
        }
    }
}
