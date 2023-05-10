using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandVerificationDetails : AuditableEntity<int>
    {
        public LandVerificationDetails() 
        {
          landVerificationSignatureDatas = new HashSet<LandVerificationSignatureData>();
        }    
        public int? VillageId { get; set; }
        public int? Khasraid { get; set; }
        public int? IsActive { get; set; }
        public string AckID { get; set; }
        public Khasra GetKhasra { get; set; }
        public Acquiredlandvillage GetAcquiredlandvillage { get; set; }
       public ICollection<LandVerificationSignatureData> landVerificationSignatureDatas { get; set; }
    }
}
