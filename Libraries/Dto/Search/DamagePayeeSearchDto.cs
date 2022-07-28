using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DamagePayeeSearchDto : BaseSearchDto
    {
        public string fileno { get; set; }
        public int district { get; set; }
        public int village { get; set; }
        public int colony { get; set; }
    }
}
