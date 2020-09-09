using System;

namespace Dto.Common
{
    public class AuditableDto<T>: EntityDto<T>, IAuditableDto
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
