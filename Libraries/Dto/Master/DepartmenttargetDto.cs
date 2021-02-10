using System;
using System.Collections.Generic;
using System.Text;
using Dto.Common;
namespace Dto.Master
{
 public class DepartmenttargetDto : AuditableDto<int>
    {
        public string DepartmentId { get; set; }
        public byte? IsActive { get; set; }
    }
}