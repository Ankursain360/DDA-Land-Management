using Dto.Common;
using System;

namespace Dto.Search
{
  public  class DemandletterDateSearchDto : BaseSearchDto
    {
        public int applicationid { get; set; }
        public DateTime demanddate { get; set; }
    }
}
