using System;

namespace Libraries.Model.Common
{
    public class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}