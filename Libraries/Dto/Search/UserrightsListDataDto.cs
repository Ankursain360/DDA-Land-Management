using Dto.Common;
using System;

namespace Dto.Search
{
  public  class UserrightsListDataDto : AuditableDto<int>

    {
       public int UserId { get; set; }
        // public int? DepartmentId { get; set; }

        public string UserName { get; set; }
        public string DepartmentName { get; set; }

        public Int64 Viewright { get; set; }
        public Int64 Downloadright { get; set; }


    }
}
