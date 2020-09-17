using System;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
{
    public partial class Undersection17
    {
        public int Id { get; set; }
        public int? UnderSection6Id { get; set; }
        public string Us17number { get; set; }
        public DateTime? Us17date { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte IsActive { get; set; }
    }
}
