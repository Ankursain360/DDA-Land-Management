using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class RoleWiseModuleMappingDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModuleCategoryId { get; set; }
        public string ModuleCategoryName { get; set; }
        public int ParentId { get; set; }

    }
}
