using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class CompactorApiDto
    {
        public string MAIN_FILE_NO { get; set; }
        public string PART_FILE_NO { get; set; }
        public string SUBJECT { get; set; }
        public string NAME { get; set; }
        public int RM_NO { get; set; }
        public string CMPCTR_NO { get; set; }
        public int RW_NO { get; set; }
        public string CLMN_NO { get; set; }
        public string ALMRH_NO { get; set; }
        public string FLT_CTGRY_ID { get; set; }
        public string LCLTY_HDR { get; set; }
        public string SQNC_NO { get; set; }
        public int YR { get; set; }
    }

    public class ApiResponseCompactor
    {
        public List<CompactorApiDto> cargo { get; set; }
    }
}
