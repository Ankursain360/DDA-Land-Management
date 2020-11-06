using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class PlanningProperties:AuditableEntity<int>
    {
        public int PlanningId { get; set; }
        public int PropertyRegistrationId { get; set; }
        public byte PropertyType { get; set; }
        public byte? IsActive { get; set; }
        public virtual Planning Planning { get; set; }
        public virtual Propertyregistration PropertyRegistration { get; set; }

    }
}
