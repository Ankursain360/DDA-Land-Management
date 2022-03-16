
using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DemolitionDasboardDataDto : BaseSearchDto
    {
        public string filter { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }
    }
}
