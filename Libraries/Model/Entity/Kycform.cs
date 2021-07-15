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
        public string PlotDescription { get; set; }
        [Required(ErrorMessage = " Zone is mandatory")]
        public int? ZoneId { get; set; }
        [Required(ErrorMessage = " Locality is mandatory")]
        public int? LocalityId { get; set; }
        public string Phase { get; set; }
        public string Sector { get; set; }
        public string Block { get; set; }
        public string Pocket { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string AadhaarNo { get; set; }
        public string Relationship { get; set; }
        public string AllotteeApplicantDetailsSame { get; set; }
        public string AllotteeLicenseeName { get; set; }
        public string AllotteeLicenseeAddress { get; set; }
        public string AllotteeLicenseeMobileNo { get; set; }
        public string AllotteeLicenseeEmailId { get; set; }
        public decimal? Area { get; set; }
        public DateTime? AllotmentLetterDate { get; set; }
        public DateTime? PossessionDate { get; set; }
        public DateTime? LeaseLicenseExecutionDate { get; set; }
        public string AadhaarNoPath { get; set; }
        public string LetterPath { get; set; }
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
        public Branch Branch { get; set; }
        public Leasetype LeaseType { get; set; }
        public Locality Locality { get; set; }
        public PropertyType PropertyType { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Kycleasepaymentrpt> Kycleasepaymentrpt { get; set; }
        public ICollection<Kyclicensepaymentrpt> Kyclicensepaymentrpt { get; set; }
    }
}
