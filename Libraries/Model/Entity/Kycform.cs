using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Kycform : AuditableEntity<int>
    {

        public Kycform()
        {
            Kycleasepaymentrpt = new HashSet<Kycleasepaymentrpt>();
            Kyclicensepaymentrpt = new HashSet<Kyclicensepaymentrpt>();

        }

        [Required(ErrorMessage = " Property field is mandatory")]
        public string Property { get; set; }

        [Required(ErrorMessage = " Nature of Property is mandatory")]
        public int? PropertyTypeId { get; set; }
        [Required(ErrorMessage = " File No is mandatory")]
        public string FileNo { get; set; }
        [Required(ErrorMessage = " Branch is mandatory")]
        public int? BranchId { get; set; }
        public int? LeaseTypeId { get; set; }
        public DateTime? TenureFrom { get; set; }
        public DateTime? TenureTo { get; set; }
        public DateTime? LicenseFrom { get; set; }
        public DateTime? LicenseTo { get; set; }
        [Required(ErrorMessage = " Plot No is mandatory")]
        public string PlotNo { get; set; }
        public string Far { get; set; }
        [Required(ErrorMessage = " Block is mandatory")]
        public string Block { get; set; }
        [Required(ErrorMessage = " Pocket is mandatory")]
        public string Pocket { get; set; }
        [Required(ErrorMessage = " Sector is mandatory")]
        public string Sector { get; set; }
        [Required(ErrorMessage = " Phase is mandatory")]
        public string Phase { get; set; }
        [Required(ErrorMessage = " Locality is mandatory")]
        public int? LocalityId { get; set; }
        [Required(ErrorMessage = " Zone is mandatory")]
        public int? ZoneId { get; set; }
        [Required(ErrorMessage = " Area is mandatory")]
        public decimal? AreaSqmt { get; set; }
        [Required(ErrorMessage = " Date of Allotment Letter is mandatory")]
        public DateTime? AllotmentLetterDate { get; set; }
        [Required(ErrorMessage = " Date of Possession is mandatory")]
        public DateTime? PossessionDate { get; set; }
        public DateTime? LeaseExecutionDate { get; set; }
        public DateTime? GroundRentPayableFromDate { get; set; }
        public decimal? LandPremiumPaid { get; set; }
        public decimal? GroundRentPayableasonDate { get; set; }
        [Required(ErrorMessage = " First Name of the Allottee is mandatory")]
        public string AllotteeFirstName { get; set; }
        public string AllotteeMiddleName { get; set; }
        public string AllotteeLastName { get; set; }
        [Required(ErrorMessage = " Address is mandatory")]
        public string Address { get; set; }
        public string PinCode { get; set; }
        [Required(ErrorMessage = " Phone No is mandatory")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = " Email Id is mandatory")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = " Branch is mandatory")]
        public string PayeeName { get; set; }
        [Required(ErrorMessage = " Payee is mandatory")]
        public string PayeeType { get; set; }
        [Required(ErrorMessage = " Depositor Name is mandatory")]
        public string DepositorName { get; set; }
        [Required(ErrorMessage = "Address is mandatory")]
        public string PayeeAddress { get; set; }
        public string PayeeEmail { get; set; }
        [Required(ErrorMessage = "Pincode is mandatory")]
        public string PayeePincode { get; set; }
        [Required(ErrorMessage = "Aadhaar No is mandatory")]
        public string PayeeAadhaarNo { get; set; }
        public string PayeePanno { get; set; }
        [Required(ErrorMessage = "Mobile No is mandatory")]
        public string PayeeMobileNo { get; set; }
        public decimal? UpfrontLicenseFeePaid { get; set; }
        public decimal? SecurityDepositPaid { get; set; }
        public DateTime? LicenseExecutionDate { get; set; }
        public DateTime? LicenseFeepayableFrom { get; set; }
        public decimal? LicenseFeepayableAsOnDate { get; set; }
        public byte IsActive { get; set; }


        [NotMapped]
        public List<Leasetype> LeasetypeList { get; set; }

        [NotMapped]
        public List<Branch> BranchList { get; set; }

        [NotMapped]
        public List<PropertyType> PropertyTypeList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }

        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        //********* rpt fields *******///

        [NotMapped]
        public List<string> ChallanNo { get; set; }

        [NotMapped]
        public List<DateTime?> PaymentDate { get; set; }
        [NotMapped]
        public List<decimal?> PaymentAmount { get; set; }
        [NotMapped]
        public List<string> BankName { get; set; }
        [NotMapped]
        public List<string> Purpose { get; set; }
        [NotMapped]
        public List<string> PaymentPeriod { get; set; }

        [NotMapped]
        public List<IFormFile> PaymentDocument { get; set; }
        [NotMapped]
        public List<string> PaymentDocPath { get; set; }

        //******
        public Branch Branch { get; set; }
        public Leasetype LeaseType { get; set; }
        public Locality Locality { get; set; }
        public PropertyType PropertyType { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Kycleasepaymentrpt> Kycleasepaymentrpt { get; set; }
        public ICollection<Kyclicensepaymentrpt> Kyclicensepaymentrpt { get; set; }
    }
}
