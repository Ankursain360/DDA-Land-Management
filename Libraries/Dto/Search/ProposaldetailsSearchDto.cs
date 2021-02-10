using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
 
      public class ProposaldetailsSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public string requiredAgency { get; set; }
       
        public string proposalFileNo { get; set; }
        //public DateTime? proposalDate { get; set; }
    }
}
