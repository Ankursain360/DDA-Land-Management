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

        public int TotalFiles { get; set; }
        public int IssuedFiles { get; set; }
        public int UnissuedFiles { get; set; }
        


    }
}
