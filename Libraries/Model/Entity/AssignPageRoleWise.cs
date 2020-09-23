using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class AssignPageRoleWise:AuditableEntity<int>
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public int PageId { get; set; }
        public byte? RDisplay { get; set; }
        public byte? RAdd { get; set; }
        public byte? REdit { get; set; }
        public byte? RDelete { get; set; }
        public byte? RView { get; set; }
        public byte? IsActive { get; set; }
        
    }
}
