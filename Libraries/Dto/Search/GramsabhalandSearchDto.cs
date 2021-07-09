using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  public  class GramsabhalandSearchDto : BaseSearchDto
    {
        public string zone { get; set; }
        public string village { get; set; }
        public string khasra { get; set; }
    }
}
