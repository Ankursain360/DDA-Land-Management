using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class IssueReturnFileSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int?  FileNo { get; set; }
        public int? filedoc { get; set; }
        

    }
}
