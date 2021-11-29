using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Damagepayeeregister : AuditableEntity<int>
    {
        public Damagepayeeregister()
        {
            Allottetype = new HashSet<Allottetype>();
            Damagepayeepersonelinfo = new HashSet<Damagepayeepersonelinfo>();
            Damagepaymenthistory = new HashSet<Damagepaymenthistory>();
            Mutationdetails = new HashSet<Mutationdetails>();
        }
        public string RefNo { get; set; }

        [Required(ErrorMessage = "File No is mandatory")]
        public string FileNo { get; set; }
        public string TypeOfDamageAssessee { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        [Required(ErrorMessage = "Property No is mandatory")]
        public string PropertyNo { get; set; }
        [Required(ErrorMessage = "Locality is mandatory")]
        public int? LocalityId { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string FloorNo { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string StreetNo { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        [Required(ErrorMessage = "Pincode is mandatory")]
        public string PinCode { get; set; }
        [Required(ErrorMessage = "District is mandatory")]
        public int? DistrictId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        [Required(ErrorMessage = "Plot Area in Sq.yds is mandatory")]
        public decimal? PlotAreaSqYard { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        [Required(ErrorMessage = "Floor Area in Sq. yds is mandatory")]
        public decimal? FloorAreaSqYard { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        [Required(ErrorMessage = "Plot Area in Sq.yds is mandatory")]

        public decimal? PlotAreaSqMt { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        [Required(ErrorMessage = "Floor Area in Sq. yds is mandatory")]
        public decimal? FloorAreaSqMt { get; set; }
        public string UseOfProperty { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ResidentialSqYard { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? ResidentialSqMt { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? CommercialSqYard { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? CommercialSqMt { get; set; }
        public string LitigationStatus { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed")]
        public string CourtName { get; set; }
       [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string CaseNo { get; set; }
      [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string OppositionName { get; set; }
        public string PetitionerRespondent { get; set; }
        public string IsDdadamagePayee { get; set; }
        public int? IsApplyForMutation { get; set; }
        public string ShowCauseNoticePath { get; set; }
        public string DocumentName { get; set; }
        public string FgformPath { get; set; }
        public string IsDocumentFor { get; set; }
        public string DocumentForFilePath { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? InterestDueAmountCompund { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? TotalValueWithInterest { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? Rebate { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? TotalPayable { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? CalculatorValue { get; set; }
        public int? Declaration1 { get; set; }
        public int? Declaration2 { get; set; }
        public int? Declaration3 { get; set; }
        public int? Otp { get; set; }
        public int? ProceedToPay { get; set; }
        public string Signature { get; set; }
        public string Achknowledgement { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public int? ApprovalZoneId { get; set; }
        public int? UserId { get; set; }
        public byte? IsActive { get; set; }
        public string PropertyPhotoPath { get; set; }

        public string AtsfilePath { get; set; }
        public string GpafilePath { get; set; }
        public string MutationFilePath { get; set; }
        public string WillFilePath { get; set; }
       
        public District District { get; set; }
        public Locality Locality { get; set; }
        [NotMapped]
        public List<District> DistrictList { get; set; }
       
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public IFormFile PropertyPhoto { get; set; }
        [NotMapped]
        public IFormFile ShowCauseNotice { get; set; }

        [NotMapped]
        public IFormFile ShowCauseNotice1 { get; set; }

        [NotMapped]
        public IFormFile DocumentIFormFile { get; set; }

        [NotMapped]
        public IFormFile Fgform { get; set; }
        [NotMapped]
        public IFormFile DocumentForFile { get; set; }
       
        [NotMapped]
        public IFormFile ATSFile { get; set; }
        [NotMapped]
        public IFormFile GPAFile { get; set; }
        [NotMapped]
        public IFormFile MutationFile { get; set; }
        [NotMapped]
        public IFormFile WillFile { get; set; }

        public ICollection<Mutationdetails> Mutationdetails { get; set; }
        public ICollection<Allottetype> Allottetype { get; set; }
        public ICollection<Damagepayeepersonelinfo> Damagepayeepersonelinfo { get; set; }
        public ICollection<Damagepaymenthistory> Damagepaymenthistory { get; set; }


        //****** ALLOTTE temp TYPE *****
   
        [NotMapped]

        public List<string> Name { get; set; }
        [NotMapped]
        public List<string> FatherName { get; set; }
        [NotMapped]
        public List<DateTime?> Date { get; set; }

        [NotMapped]
        public List<IFormFile> ATSGPA { get; set; }
        [NotMapped]
        public List<string> ATSGPAFilePath { get; set; }

        //****** Damage payee personal info temp *****
        [Required(ErrorMessage = "Name is mandatory")]
        [NotMapped]
      
        public List<string> payeeName { get; set; }
        [Required(ErrorMessage = "Father/Husband Name is mandatory")]
        [NotMapped]
      
        public List<string> payeeFatherName { get; set; }
        [Required(ErrorMessage = "Gender is mandatory")]
        [NotMapped]
      
        public List<string> Gender { get; set; }
        [Required(ErrorMessage = "Address is mandatory")]
        [NotMapped]
      
        public List<string> Address { get; set; }
        [Required(ErrorMessage = "Mobile No is mandatory")]
        [NotMapped]
      
        public List<string> MobileNo { get; set; }
        [Required(ErrorMessage = "Email is mandatory")]
        [NotMapped]
      
        public List<string> EmailId { get; set; }
        [Required(ErrorMessage = "Aadhar No is mandatory")]
        [NotMapped]
     
        public List<string> AadharNo { get; set; }
        [NotMapped]

        public List<string> AadharNoFilePath { get; set; }
        [NotMapped]
        public List<IFormFile> Aadhar { get; set; }

        [NotMapped]
        public IFormFile Aadhar1 { get; set; }


        [NotMapped]
        public IFormFile Receipt1 { get; set; }

        [NotMapped]      
        public List<string> PanNo { get; set; }

        [NotMapped]
        public IFormFile PanNo1 { get; set; }

        [NotMapped]
        public List<string> PanNoFilePath { get; set; }

        [NotMapped]
        public IFormFile PanNoFilePath1 { get; set; }

        [NotMapped]
        public List<IFormFile> Pan { get; set; }

        [NotMapped]
        public IFormFile Pan1 { get; set; }

        [NotMapped]
        public List<IFormFile> Photograph { get; set; }
        [Required(ErrorMessage = "Photo is mandatory")]

        //[NotMapped]
        //public IFormFile Photograph1 { get; set; }

        [NotMapped]
        public List<string> PhotographFilePath { get; set; }

        [NotMapped]
        public IFormFile PhotographFilePath1 { get; set; }

        [NotMapped]
        public List<IFormFile> SignatureFile { get; set; }
       // [Required(ErrorMessage = "Signature is mandatory")]

        //[NotMapped]
        //public IFormFile SignatureFile1 { get; set; }

        [NotMapped]
        public List<string> SignatureFilePath { get; set; }
        //[NotMapped]
        //public List<IFormFile> OtherDocFile { get; set; }

        //[NotMapped]
        //public List<string> OtherDocFilePath { get; set; }


        //****** Damagepaymenthistory temp ***
      
        [NotMapped]

        public List<string> PaymntName { get; set; }
        [NotMapped]
       
        public List<string> RecieptNo { get; set; }
        [NotMapped]
      
        public List<string> PaymentMode { get; set; }
        [NotMapped]
        public List<DateTime?> PaymentDate { get; set; }
        [NotMapped]
       
        public List<decimal?> Amount { get; set; }


        [NotMapped]
        public List<IFormFile> Reciept { get; set; }
        [NotMapped]

        public List<string> RecieptFilePath { get; set; }

        //******* Mutation  **********//
        [NotMapped]
        public List<Locality> PropLocalityList { get; set; }
        [NotMapped]
        public List<District> PropDistrictList { get; set; }
        [NotMapped]
        public List<Damagepayeepersonelinfo> PersonalInfoDamageList { get; set; }
        [NotMapped]
        public List<Allottetype> AlloteeTypeDamageList { get; set; }
        [NotMapped]
        public List<Damagepayeeregister> DamagePayeeRegisterList { get; set; }

        [NotMapped]
        public bool DeclarationStatus1 { get; set; }
        [NotMapped]
        public bool DeclarationStatus2 { get; set; }
        [NotMapped]
        public bool DeclarationStatus3 { get; set; }
        [NotMapped]
        public int IsMutaionYes { get; set; }

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
        public string EncryptData { get; set; }
        [NotMapped]
        public int ApprovalRoleId { get; set; }

        public Approvalstatus ApprovedStatusNavigation { get; set; }
    }
}
