using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class DepartmentDto : AuditableDto<int>
    {
        public string Name { get; set; }
        public byte? IsActive { get; set; }
    }
}
