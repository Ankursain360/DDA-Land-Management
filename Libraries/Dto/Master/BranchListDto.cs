using Dto.Common;
using System;
using System.Collections.Generic;
namespace Dto.Master
{
    public class BranchListDto
    {

        public int Id { get; set; }
        public string Department { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
       
        public string Status { get; set; }

    }
}
