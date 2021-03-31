using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Leasedeed : AuditableEntity<int>
    {
       
        public int AllotmentId { get; set; }
        public DateTime LeaseDeedDate { get; set; }
        public string DocumentPath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
       

        public Allotmententry Allotment { get; set; }
    }
}
