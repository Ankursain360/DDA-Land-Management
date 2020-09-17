using System;
using System.Collections.Generic;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
{
    public partial class Awardplotdetails
    {
        public int Id { get; set; }
        public int AwardMasterId { get; set; }
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
