using System;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
{
    public partial class Khewat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
