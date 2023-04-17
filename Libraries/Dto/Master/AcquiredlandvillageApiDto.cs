using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class AcquiredlandvillageApiDto
    {
        public int villageID { get; set; } 
        public string village_NAME { get; set; }
        //public DateTime CREATIONDATE { get; set; }
        //public int CREATEDBY { get; set; }
    }
    public class AcquiredlandvillageResponseDetails 
    { 
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<AcquiredlandvillageApiDto> response { get; set; }
    }
}
