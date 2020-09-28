using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Undersection6 : AuditableEntity<int>
    {

        public string UnderSectionNotificationNumber { get; set; }
        public DateTime? UnderSectionDate { get; set; }
        public byte? IsActive { get; set; }

    }
}