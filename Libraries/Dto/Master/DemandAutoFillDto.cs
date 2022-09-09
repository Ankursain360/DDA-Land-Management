using Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class DemandAutoFillDto : AuditableDto<int>
    { 
        public string propertyNo { get; set; }
        public string plotAreaSqYard { get; set; }
        public string payeeName { get; set; }
        public string fatherName { get; set; }
        public string address { get; set; }
    }
}
