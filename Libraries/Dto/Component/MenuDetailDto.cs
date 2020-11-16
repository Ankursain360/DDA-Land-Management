using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Component
{
    public class MenuDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int? SortOrder { get; set; }
        public string Url { get; set; }
    }
}
