using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class DemolitionPoliceLetterDto
    {
        public string InspectionDate { get; set; }
        public string Department { get; set; }
        public string Zone { get; set; }
        public string KhasraNo_PlotNo { get; set; } 
        public string Status { get; set; }
        public string LetterStatus { get; set; }
    }
}
