using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DemolitionstructuredetailsDto : BaseSearchDto
    {
        public string name { get; set; }
        // public string FileNo { get; set; }
        public int StatusId { get; set; }
    }
}
