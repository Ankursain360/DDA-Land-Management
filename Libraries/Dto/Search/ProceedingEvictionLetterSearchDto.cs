using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class ProceedingEvictionLetterSearchDto : BaseSearchDto
    {
        public int RefNoNameId { get; set; }
        public string LetterReferenceNo { get; set; }
    }
}
