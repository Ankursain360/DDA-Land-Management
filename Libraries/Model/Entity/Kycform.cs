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
            Kycdemandpaymentdetails = new HashSet<Kycdemandpaymentdetails>();
            Kycdemandpaymentdetailstablea = new HashSet<Kycdemandpaymentdetailstablea>();
            Kycdemandpaymentdetailstableb = new HashSet<Kycdemandpaymentdetailstableb>();
            Kycdemandpaymentdetailstablec = new HashSet<Kycdemandpaymentdetailstablec>();

        }


       [Required(ErrorMessage = " Property field is mandatory")]
        public string Property { get; set; }
       // [Required(ErrorMessage = " Nature of Property is mandatory")]
        public int? PropertyTypeId { get; set; }
      //  [Required(ErrorMessage = " File No is mandatory")]
        public string FileNo { get; set; }
        [Required(ErrorMessage = " Branch is mandatory")]
        public int? BranchId { get; set; }
     //   [Required(ErrorMessage = "Lease Ground Rent is mandatory")]
        public string LeaseGroundRentDepositFrequency { get; set; }
       // [Required(ErrorMessage = " License Frequency is mandatory")]
        public string LicenseFrequency { get; set; }
       // [Required(ErrorMessage = " Lease Type is mandatory")]
        public int? LeaseTypeId { get; set; }
        public DateTime? TenureFrom { get; set; }
        public DateTime? TenureTo { get; set; }
     //   [Required(ErrorMessage = " License Period From is mandatory")]
        public DateTime? LicenseFrom { get; set; }
      //  [Required(ErrorMessage = " License Period To is mandatory")]
        public DateTime? LicenseTo { get; set; }
      //  [Required(ErrorMessage = " Plot No is mandatory")]
        public string PlotNo { get; set; }
       // [Required(ErrorMessage = " Plot Description is mandatory")]
        public string PlotDescription { get; set; }

       // [Required(ErrorMessage = " Zone is mandatory")]
        public int? ZoneId { get; set; }
      //  [Required(ErrorMessage = " Locality is mandatory")]
        public int? LocalityId { get; set; }
      //  [Required(ErrorMessage = " Phase is mandatory")]
        public string Phase { get; set; }
     //   [Required(ErrorMessage = " Sector is mandatory")]
        public string Sector { get; set; }
      //  [Required(ErrorMessage = " Block is mandatory")]
        public string Block { get; set; }
     //   [Required(ErrorMessage = " Pocket is mandatory")]
        public string Pocket { get; set; }
     //   [Required(ErrorMessage = " Name is mandatory")]
        public string Name { get; set; }
        public string FatherName { get; set; }
      //  [Required(ErrorMessage = " Gender is mandatory")]
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
     //   [Required(ErrorMessage = " Aadhaar No is mandatory")]
        public string AadhaarNo { get; set; }
     //   [Required(ErrorMessage = " Relationship with Allottee is mandatory")]
        public string Relationship { get; set; }
        public string AllotteeApplicantDetailsSame { get; set; }
      //  [Required(ErrorMessage = "Name of the Allottee is mandatory")]
        public string AllotteeLicenseeName { get; set; }
      //  [Required(ErrorMessage = " Correspondence Address is mandatory")]
        public string AllotteeLicenseeAddress { get; set; }
     //   [Required(ErrorMessage = " Mobile No  is mandatory")]
        public string AllotteeLicenseeMobileNo { get; set; }
        public string AllotteeLicenseeEmailId { get; set; }
     //   [Required(ErrorMessage = "Area  is mandatory")]
        public decimal? Area { get; set; }
      //  [Required(ErrorMessage = "Area Unit  is mandatory")]
        public string AreaUnit { get; set; }
        public DateTime? AllotmentLetterDate { get; set; }
      //  [Required(ErrorMessage = "Date of Possession is mandatory")]
        public DateTime? PossessionDate { get; set; }
        public DateTime? LeaseLicenseExecutionDate { get; set; }
        public decimal? LandPremiumAmount { get; set; }
        public decimal? GroundRentAmount { get; set; }
      //  [Required(ErrorMessage = "License Fee is mandatory")]
        public decimal? LicenseFeePayable { get; set; }
        public string AadhaarNoPath { get; set; }
        public string LetterPath { get; set; }
        public string AadhaarPanapplicantPath { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public string KycStatus { get; set; }
        public byte IsActive { get; set; }

        #region Approval Related Fields
        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public int ApprovalStatusCode { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }

        [NotMapped]
        public IFormFile ApprovalDocument { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalStatusList { get; set; }

        [NotMapped]
        public int ApprovalUserId { get; set; }

        [NotMapped]
        public int ApprovalRoleId { get; set; }

        #endregion
       
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

        ////********* rpt fields *******///

        //[NotMapped]
        //public List<string> ChallanNo { get; set; }

        //[NotMapped]
        //public List<DateTime?> PaymentDate { get; set; }
        //[NotMapped]
        //public List<decimal?> PaymentAmount { get; set; }
        //[NotMapped]
        //public List<string> BankName { get; set; }
        //[NotMapped]
        //public List<string> Purpose { get; set; }
        //[NotMapped]
        //public List<string> PaymentPeriod { get; set; }

        //[NotMapped]
        //public List<IFormFile> PaymentDocument { get; set; }
        //[NotMapped]
        //public List<string> PaymentDocPath { get; set; }

        //******

        [NotMapped]
        public IFormFile Aadhar { get; set; }
        [NotMapped]
        public IFormFile Letter { get; set; }
        [NotMapped]
        public IFormFile ApplicantPan { get; set; }
        public Approvalstatus ApprovedStatusNavigation { get; set; }
        public Branch Branch { get; set; }
        public Leasetype LeaseType { get; set; }
        public Locality Locality { get; set; }
        public PropertyType PropertyType { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Kycleasepaymentrpt> Kycleasepaymentrpt { get; set; }
        public ICollection<Kyclicensepaymentrpt> Kyclicensepaymentrpt { get; set; }
        public ICollection<Leasesignup> Leasesignup { get; set; }
        public ICollection<Kycdemandpaymentdetails> Kycdemandpaymentdetails { get; set; }
        public ICollection<Kycdemandpaymentdetailstablea> Kycdemandpaymentdetailstablea { get; set; }
        public ICollection<Kycdemandpaymentdetailstableb> Kycdemandpaymentdetailstableb { get; set; }
        public ICollection<Kycdemandpaymentdetailstablec> Kycdemandpaymentdetailstablec { get; set; }
    }
}
