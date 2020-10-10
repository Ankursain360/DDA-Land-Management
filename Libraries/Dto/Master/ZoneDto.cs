using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ZoneDto : AuditableDto<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public byte IsActive { get; set; }
    }
}
