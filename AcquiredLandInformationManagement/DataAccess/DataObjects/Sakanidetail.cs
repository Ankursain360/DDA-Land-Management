using System;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
{
    public partial class Sakanidetail
    {
        public int Id { get; set; }
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public DateTime? YearOfJamabandi { get; set; }
        public int KhewatId { get; set; }
        public string Location { get; set; }
        public string OwnerDetails { get; set; }
        public string LeaseDetails { get; set; }
        public string Tenant { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
