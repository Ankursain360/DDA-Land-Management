using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Damagepayeeregistertemp : AuditableEntity<int>
    {
        public Damagepayeeregistertemp()
        {
            Allottetypetemp = new HashSet<Allottetypetemp>();
            Damagepayeepersonelinfotemp = new HashSet<Damagepayeepersonelinfotemp>();
            Damagepaymenthistorytemp = new HashSet<Damagepaymenthistorytemp>();
            Mutationdetails = new HashSet<Mutationdetails>();
        }
        [Required(ErrorMessage = "File No is mandatory feild")]
        public string FileNo { get; set; }
        public string TypeOfDamageAssessee { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string PropertyNo { get; set; }
        public int? LocalityId { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string FloorNo { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
        public string StreetNo { get; set; }
        [StringLength(45,ErrorMessage = "Maximum 45 characters allowed ")]
       
        public string PinCode { get; set; }
        public int? DistrictId { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? PlotAreaSqYard { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? FloorAreaSqYard { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal? PlotAreaSqMt { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
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
        public int? PendingAt { get; set; }
        public int? UserId { get; set; }
        public byte? IsActive { get; set; }
        public string PropertyPhotoPath { get; set; }

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
        public IFormFile Fgform { get; set; }
        [NotMapped]
        public IFormFile DocumentForFile { get; set; }
        public ICollection<Mutationdetails> Mutationdetails { get; set; }
        public ICollection<Allottetypetemp> Allottetypetemp { get; set; }
        public ICollection<Damagepayeepersonelinfotemp> Damagepayeepersonelinfotemp { get; set; }
        public ICollection<Damagepaymenthistorytemp> Damagepaymenthistorytemp { get; set; }


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
        [NotMapped]
      
        public List<string> payeeName { get; set; }
        [NotMapped]
      
        public List<string> payeeFatherName { get; set; }
        [NotMapped]
      
        public List<string> Gender { get; set; }
        [NotMapped]
      
        public List<string> Address { get; set; }
        [NotMapped]
      
        public List<string> MobileNo { get; set; }
        [NotMapped]
      
        public List<string> EmailId { get; set; }
        [NotMapped]
     
        public List<string> AadharNo { get; set; }
        [NotMapped]

        public List<string> AadharNoFilePath { get; set; }
        [NotMapped]
        public List<IFormFile> Aadhar { get; set; }
        [NotMapped]      
        public List<string> PanNo { get; set; }
        [NotMapped]
        public List<string> PanNoFilePath { get; set; }
        [NotMapped]
        public List<IFormFile> Pan { get; set; }
        [NotMapped]
        public List<IFormFile> Photograph { get; set; }
        [NotMapped]
        public List<string> PhotographFilePath { get; set; }
        [NotMapped]
        public List<IFormFile> SignatureFile { get; set; }

        [NotMapped]
        public List<string> SignatureFilePath { get; set; }


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
        public List<Damagepayeepersonelinfotemp> PersonalInfoDamageList { get; set; }
        [NotMapped]
        public List<Allottetypetemp> AlloteeTypeDamageList { get; set; }
        [NotMapped]
        public List<Damagepayeeregistertemp> DamagePayeeRegisterList { get; set; }

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
        public string ApprovalRemarks { get; set; }
    }
}
