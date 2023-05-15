using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class landverificationdetailsDto
    {
        public int Id { get; set; }
        public int? VillageId { get; set; }
        public int? Khasraid { get; set; }
        public string AckID { get; set; }
        public List<SignatureDetails> signatureData { get; set; }
        public int LandVerificationSignatureId { get; set; }
        public int createdby { get; set; }
    }
    public class landverificationResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string AckID { get; set; }
        public List<landverificationdetailsDto> response { get; set; }
    }
    public class SignatureDetails
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
        public int createdby { get; set; }
        public List<VillageDetails> villagedetails { get; set; }
    }

    public class VillageDetails
    {
        public string villageName { get; set; }
        public string khasra_No { get; set; }
        public string areaBhigha_Biswa_Biswana { get; set; }
        public string notification_s_US_4 { get; set; }
        public string notification_s_US_6 { get; set; }
        public string notification_s_US_17 { get; set; }
        public string notification_s_US_22 { get; set; }
        public int createdby { get; set; }
    }
}
