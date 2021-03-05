using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Newlandannexure1khasrarpt : AuditableEntity<int>
    {
   
       
        public int NewLandAnnexure1Id { get; set; }
        public string KhasraNo { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string OwnershipStatus { get; set; }
        public string OwnerName { get; set; }
       
        public byte IsActive { get; set; }

        public Newlandannexure1 NewLandAnnexure1 { get; set; }
    }
}
