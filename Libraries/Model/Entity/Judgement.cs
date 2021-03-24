
using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Judgement : AuditableEntity<int>
    {

      
        public int? RequestforProceedingId { get; set; }
        public int? ForwardTo { get; set; }
        public string DocumentPath { get; set; }
        public byte? IsActive { get; set; }
      
    }
}

