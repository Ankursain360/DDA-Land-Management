using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Kycform : AuditableEntity<int>
    {
       
        public string Property { get; set; }
        public string PropertyNature { get; set; }
        public string FileNo { get; set; }
        public string Branch { get; set; }
        public string LeaseType { get; set; }
        public DateTime? TenureFrom { get; set; }
        public DateTime? TenureTo { get; set; }
        public DateTime? LicenseFrom { get; set; }
        public DateTime? LicenseTo { get; set; }
        public string PlotNo { get; set; }
        public string Far { get; set; }
        public string Block { get; set; }
        public string Pocket { get; set; }
        public string Sector { get; set; }
        public string Phase { get; set; }
        public int? LocalityId { get; set; }
        public int? ZoneId { get; set; }
        public decimal? AreaSqmt { get; set; }
        public DateTime? AllotmentLetterDate { get; set; }
        public DateTime? PossessionDate { get; set; }
        public DateTime? LeaseExecutionDate { get; set; }
        public DateTime? GroundRentPayableFromDate { get; set; }
        public decimal? LandPremiumPaid { get; set; }
        public decimal? GroundRentPayableasonDate { get; set; }
        public string AllotteeFirstName { get; set; }
        public string AllotteeMiddleName { get; set; }
        public string AllotteeLastName { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string PayeeName { get; set; }
        public string PayeeType { get; set; }
        public string DepositorName { get; set; }
        public string PayeeAddress { get; set; }
        public string PayeeEmail { get; set; }
        public string PayeePincode { get; set; }
        public string PayeeAadhaarNo { get; set; }
        public string PayeePanno { get; set; }
        public string PayeeMobileNo { get; set; }
        public decimal? UpfrontLicenseFeePaid { get; set; }
        public decimal? SecurityDepositPaid { get; set; }
        public DateTime? LicenseExecutionDate { get; set; }
        public DateTime? LicenseFeepayableFrom { get; set; }
        public decimal? LicenseFeepayableAsOnDate { get; set; }
        public byte IsActive { get; set; }
       

        public Locality Locality { get; set; }
        public Zone Zone { get; set; }
    }
}
