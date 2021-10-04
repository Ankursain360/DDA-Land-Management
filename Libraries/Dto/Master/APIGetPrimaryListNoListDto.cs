using System;

using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class APIGetPrimaryListNoListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
    }

    public class APIGetPrimaryListNoResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<APIGetPrimaryListNoListDto> APIGetPrimaryListNoListDto { get; set; }
    }
}
