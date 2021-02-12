using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
 
      public class DisposalLandSearchDto : BaseSearchDto
    {
        public string village { get; set; }

        public string khasra { get; set; }

        public string utilizationtype { get; set; }
    }
}
