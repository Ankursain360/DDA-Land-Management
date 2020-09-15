using System;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
{
    public partial class Undersection4
    {
        public int Id { get; set; }
        public int PurposeId { get; set; }
        public string Number { get; set; }
        public DateTime? Ndate { get; set; }
        public string Npurpose { get; set; }
        public string TypeDetails { get; set; }
        public string TypePurpose { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
