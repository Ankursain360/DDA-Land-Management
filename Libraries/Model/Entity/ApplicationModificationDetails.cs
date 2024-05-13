using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class ApplicationModificationDetails:AuditableEntity<int>
    {
        public string moduleName { get; set; }
        public string changeLog { get; set; }
        public DateTime updated { get; set; } 
        public byte? Isactive { get; set; } 
        public string UpdatedBy { get; set; }
    }
}
