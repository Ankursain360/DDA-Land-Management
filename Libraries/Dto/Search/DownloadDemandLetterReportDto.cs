
using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class DownloadDemandLetterReportDto 
    {
        public int PropertyNo { get; set; }

        public string FileNo { get; set; }
        public int Locality { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        
    }
}
