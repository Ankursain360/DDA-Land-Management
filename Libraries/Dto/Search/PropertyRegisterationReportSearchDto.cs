using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PropertyRegisterationReportSearchDto : BaseSearchDto
    {
        public string name { get; set; }
        public int inventoriedIn{ get; set; }
        public int classificationofland { get; set; }
        public int department { get; set; }
        public int zone { get; set; }
        public int division { get; set; }
        public int locality { get; set; }
        public string plannedUnplannedLand { get; set; }
        public int mainLandUse { get; set; }
        public int litigation { get; set; }
        public int encroached { get; set; }
        public string khasraNo { get; set; }
        public string colony { get; set; }
        public string sector { get; set; }
        public string block { get; set; }
        public string pocket { get; set; }
        public string plotNo { get; set; }



    }
}
