using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class DemolitionReportListDto
    {
        public string Department { get; set; }
        public string Zone { get; set; }
        public string Division { get; set; }
        public string Locality_VillageName { get; set; }
        public string khasra_PlotNo { get; set; }
        public string DemolitionDate { get; set; }
        public string AreaReclaimed_Sq_mtr { get; set; } 
        public string ReasonofDemolition { get; set; }
    }
}
