using System;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
{
    public partial class Scheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? SchemeDate { get; set; }
        public string FileNo { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
