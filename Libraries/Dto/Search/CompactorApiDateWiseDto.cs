using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dto.Search
{
   public class CompactorApiDateWiseDto 

    {
        public int SNO { get; set; }
        public string DEPT_NAME { get; set; }
        public int BSNO { get; set; }
        public string BRANCH_NAME { get; set; }
        public int TOTAL { get; set; }
        public int ISSUED { get; set; }
        public int UNISSUED { get; set; }


    }
    public class ApiResponseCompactorDateWise
    {
        public List<CompactorApiDateWiseDto> cargo { get; set; }


    }
}
