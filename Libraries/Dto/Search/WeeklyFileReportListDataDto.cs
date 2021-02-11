using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
  public  class WeeklyFileReportListDataDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; } 
        public int FilesToBeDone { get; set; }
        public Int64 totalFilesDone { get; set; }
        public int WeeklyToBeDone { get; set; }
        public Int64 weeklyFilesDone { get; set; }
         public String DeptName { get; set; }
    }
}
