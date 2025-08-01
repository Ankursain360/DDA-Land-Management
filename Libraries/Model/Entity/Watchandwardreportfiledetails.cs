﻿using Libraries.Model.Common;
using System;
using System.Collections.Generic;


namespace Libraries.Model.Entity
{
    public partial class Watchandwardreportfiledetails : AuditableEntity<int>
    {
      
        public int WatchAndWardId { get; set; }
        public string ReportFilePath { get; set; }
        public byte IsActive { get; set; }
       

        public virtual Watchandward WatchAndWard { get; set; }
    }
}
