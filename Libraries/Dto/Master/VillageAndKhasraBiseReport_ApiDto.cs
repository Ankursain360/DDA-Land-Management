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
        public string Notification_s_US_6 { get; set; }
        public string Notification_s_US_17 { get; set; }
        public string Notification_s_US_22 { get; set; }
        public string Awards  { get; set; }
        public string Date_of_Possesion { get; set; }

        public class VillageAndKhasraBiseReportResponseDetails 
        {
            public string responseCode { get; set; }
            public string responseMessage { get; set; }
            public List<VillageAndKhasraBiseReport_ApiDto> response { get; set; }
        }
    }
}
