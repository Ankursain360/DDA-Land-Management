

using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class KycformSearchDto : BaseSearchDto
    {
       
        public string Mobileno { get; set; }
        public string Id { get; set; }
        public string PlotNo { get; set; }
        public string property { get; set; }
        public string Fileno { get; set; }
        public string zone { get; set; }
        public string locality { get; set; }
       
    }

}

