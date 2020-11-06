using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class PropertyRegistrationHistory : AuditableEntity<int>
    {
        public int DepartmentId { get; set; }
        public int ZoneId { get; set; }
        public int DivisionId { get; set; }
        public int PropertyRegistrationId { get; set; }
        public int LandTransferId { get; set; }
        public byte? IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Landtransfer LandTransfer { get; set; }
        public virtual Propertyregistration PropertyRegistration { get; set; }
        public virtual Zone Zone { get; set; }
    }
}
