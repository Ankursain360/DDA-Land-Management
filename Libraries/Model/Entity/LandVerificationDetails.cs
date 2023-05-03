using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandVerificationDetails : AuditableEntity<int>
    {
        public int? VillageId { get; set; }
        public int? Khasraid { get; set; }
        public int? signatureDataId { get; set; }
        public Acquiredlandvillage GetAcquiredlandvillage { get; set; }
        public Khasra GetKhasra { get; set; }
        public LandVerificationSignatureData GetLandVerificationSignatureData { get; set; }
    }
}
