using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Watchandward : AuditableEntity<int>
    {
       
        public DateTime? Date { get; set; }
        public int? VillageId { get; set; }
        public int? KhasraId { get; set; }
        public string Landmark { get; set; }
        public int? Encroachment { get; set; }
        public string StatusOnGround { get; set; }
        public string PhotoPath { get; set; }
        public string ReportFiletPath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
      

    }
}
