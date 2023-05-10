using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandVerificationSignatureData : AuditableEntity<int>
    {
        public LandVerificationSignatureData() 
        {
            LandVerificationVillageDetails = new HashSet<LandVerificationVillageDetails>();
        }
        public string signatureText { get; set; }
        public string signatureType { get; set; }
        public string subjectName { get; set; }
        public string EmailId { get; set; }
        public string TokenserialNo { get; set; }
        public string signature { get; set; }
        public DateTime signatureDate { get; set; }
        public string AccountName { get; set; }
        public string AccountDesignation { get; set; }
        public int? LandVerificationDetailsId { get; set; }
        public LandVerificationDetails GetLandVerificationDetail { get; set; } 
        public ICollection<LandVerificationVillageDetails> LandVerificationVillageDetails { get; set; }

        [NotMapped]
        public List<string> signatureTextlist { get; set; }

        [NotMapped]
        public List<string> signatureTypelist { get; set; }

        [NotMapped]
        public List<string> subjectNamelist { get; set; }

        [NotMapped]
        public List<string> EmailIdlist { get; set; }

        [NotMapped]
        public List<string> TokenserialNolist { get; set; }

        [NotMapped]
        public List<string> signaturelist { get; set; }

        [NotMapped]
        public List<string> signatureDatelist { get; set; }

        [NotMapped]
        public List<string> AccountNamelist { get; set; }

        [NotMapped]
        public List<string> AccountDesignationlist { get; set; }
    }
}
