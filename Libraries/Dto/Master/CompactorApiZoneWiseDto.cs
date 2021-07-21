using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class CompactorApiZoneWiseDto
    {
        public int SNO { get; set; }
        public string DEPT_NAME { get; set; }
        public int BSNO { get; set; }
        public string BRANCH_NAME { get; set; }
        public int TOTAL { get; set; }
        public int ISSUED { get; set; }
        public int UNISSUED { get; set; }


    }
    public class ApiResponseCompactorZoneWise
    {
        public List<CompactorApiZoneWiseDto> cargo { get; set; }


    }
}
