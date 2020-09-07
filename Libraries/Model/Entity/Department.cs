using System;
using System.Collections.Generic;
using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Department : AuditableEntity<int>
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public byte? IsActive { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }
}
