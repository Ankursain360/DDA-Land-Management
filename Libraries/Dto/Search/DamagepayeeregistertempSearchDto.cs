using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  
     public class DamagepayeeregistertempSearchDto : BaseSearchDto
    {
        public string fileno { get; set; }
        public string propertyno { get; set; }
        public int locality { get; set; }
    }
}
