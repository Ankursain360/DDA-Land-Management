using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class GetNoticeGenerationReportListDto
    {
        
        public string FileNo { get; set; }
        public string PayeeName { get; set; }
        public string PropertyNo { get; set; }
        public string Address { get; set; }
        public string No_ofNoticeGenerated_Issued { get; set; }
    }
}
