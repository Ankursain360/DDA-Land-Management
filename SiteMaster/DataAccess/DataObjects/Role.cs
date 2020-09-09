using System;
using System.Collections.Generic;

namespace SiteMaster.DataAccess.DataObjects
{
    public partial class Role
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
