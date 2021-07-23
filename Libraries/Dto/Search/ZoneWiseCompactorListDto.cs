using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
     public class ZoneWiseCompactorListDto
    {

        public int SNO { get; set; }
        public string DEPT_NAME { get; set; }
       
        public string BRANCH_NAME { get; set; }
        public int TOTAL { get; set; }
        public int ISSUED { get; set; }
        public int UNISSUED { get; set; }
    }
}
