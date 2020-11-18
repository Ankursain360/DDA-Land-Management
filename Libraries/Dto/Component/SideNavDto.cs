using System;
using System.Collections.Generic;

namespace Dto.Component
{
    public class SideNavDto
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public IList<SideNavDto> Children { get; set; }
    }
}
