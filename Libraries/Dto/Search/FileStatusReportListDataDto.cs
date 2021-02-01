using Dto.Common;
using System;

namespace Dto.Search
{
    public class FileStatusReportListDataDto
    {
        public int Id { get; set; }


        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
        public DateTime CreatedDate { get; set; }

        public long TotalFiles { get; set; }
        public long IssuedFiles { get; set; }
        public long UnissuedFiles { get; set; }
        


    }
}
