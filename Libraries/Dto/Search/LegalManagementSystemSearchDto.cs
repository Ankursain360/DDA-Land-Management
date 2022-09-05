using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
   public class LegalManagementSystemSearchDto : BaseSearchDto
    {
        public string fileNo { get; set; }
        public string lmfileno { get; set; }
        public string courtCaseNo { get; set; }
        public string courtType { get; set; }
        public string courtCaseTitle { get; set; }
        public string caseStatus { get; set; }
        public string isActive { get; set; }
    }
}
