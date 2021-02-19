using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{

    public class NewlandacquistionproposaldetailsSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string requiredAgency { get; set; }

        public string proposalFileNo { get; set; }
        
    }
}
