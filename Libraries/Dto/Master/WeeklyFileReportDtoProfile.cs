using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
   public class WeeklyFileReportDtoProfile
    {
        public int DepartmentId { get; set; }
        public int FilesToBeDone { get; set; }
        public int totalFilesDone { get; set; }
        public int WeeklyToBeDone { get; set; }
        public int weeklyFilesDone { get; set; }
        public int DeptName { get; set; }
    }
}
