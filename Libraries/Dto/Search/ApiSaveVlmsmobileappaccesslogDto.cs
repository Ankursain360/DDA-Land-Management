using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class ApiSaveVlmsmobileappaccesslogDto
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string Brand { get; set; }
        public string OSVersion { get; set; }
        public string LoginStatus { get; set; }
        public string ModelNumber { get; set; }
        public int CreatedBy { get; set; }
        public string IsActive { get; set; }
    }
    public class ApiSaveVlmsmobileappaccesslogResponseDetails
    {
        public string responseCode { get; set; } 
        public string responseMessage { get; set; }
        public List<ApiSaveVlmsmobileappaccesslogDto>  response { get; set; }
    }
}
