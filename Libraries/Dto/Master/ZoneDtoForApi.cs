using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ZoneDtoForApi
    {
        public int ZONEID { get; set; }
        public string ZONE { get; set; }
    }
    public class ZoneApiResponseDetails 
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ZoneDtoForApi> response { get; set; }
    }
}
