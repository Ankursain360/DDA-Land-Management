using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandVerificationSignatureData : AuditableEntity<int>
    {
        public string signatureText { get; set; }
        public string signatureType { get; set; }
        public string subjectName { get; set; }
        public string EmailId { get; set; }
        public string TokenserialNo { get; set; }
        public string signature { get; set; }
        public DateTime signatureDate { get; set; }
        public string AccountName { get; set; }
        public string AccountDesignation { get; set; }
        public int? LandVerificationVillageId { get; set; }
        public LandVerificationVillageDetails landVerificationVillage { get; set; }
    }
}
