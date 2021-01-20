using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DemolitionPoliceAssistenceLetterSearchDto: BaseSearchDto
    {
        public string name { get; set; }
        public int StatusId { get; set; }
        public string colname { get; set; }
        public int orderby { get; set; }

    }
}
