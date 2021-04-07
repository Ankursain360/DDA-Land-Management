using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class AllotmentLetterSeearchDto : BaseSearchDto
    {
        public string AppRefNo { get; set; }
        public DateTime GenerateDate { get; set; }
        
    }
}
