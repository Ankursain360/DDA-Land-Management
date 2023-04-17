using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class AcquiredlandKhasraApiDto
    {
        public int KhasraID { get; set; }
        public string Khasra_NAME { get; set; }
    }
    public class AcquiredlandKhasraResponseDetails 
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<AcquiredlandKhasraApiDto> response { get; set; }
    }
}
