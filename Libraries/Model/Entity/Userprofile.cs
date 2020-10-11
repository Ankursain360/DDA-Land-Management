using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public partial class Userprofile : AuditableEntity<int>
    {
        public int? RoleId { get; set; }
        public int? ZoneId { get; set; }
        public int? DistrictId { get; set; }
        public short? IsActive { get; set; }
        public int UserId { get; set; }

        public virtual District District { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Zone Zone { get; set; }
    }
}
