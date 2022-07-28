using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Libraries.Model.Entity
{
    public class Newdamagepayeeregistration : AuditableEntity<int>
    {
        public Newdamagepayeeregistration()
        {
            Newdamagepayeeoccupantinfo = new HashSet<Newdamagepayeeoccupantinfo>();
            Newdamagepaymenthistory = new HashSet<Newdamagepaymenthistory>();
            Newdamageselfassessmentatsdetail = new HashSet<Newdamageselfassessmentatsdetail>();
            Newdamageselfassessmentfloordetail = new HashSet<Newdamageselfassessmentfloordetail>();
            Newdamageselfassessmentgpadetail = new HashSet<Newdamageselfassessmentgpadetail>();
            Newdamageselfassessmentholderdetail = new HashSet<Newdamageselfassessmentholderdetail>();
        }

        [Required(ErrorMessage = "Please specify the property")]
        public string WhetherDamageProp { get; set; }
        [Required(ErrorMessage = "please specify current occupant")]
        public string Occupanttype { get; set; }
        public string IsNameChanged { get; set; }
        public string FileNo { get; set; }
        [Required(ErrorMessage = "please select District")]
        public int? DistrictId { get; set; }
        [Required(ErrorMessage = "please select Revenue eState")]
        public int? VillageId { get; set; }
        [Required(ErrorMessage = "please select Colony")]
        public int? ColonyId { get; set; }
        public string OtherColonyName { get; set; }
        public string PinCode { get; set; }
        [Required(ErrorMessage = "please choose type of property")]
        public string TypeOfProperty { get; set; }
        [Required(ErrorMessage = "please choose use type")]
        public string UseType { get; set; }
        public string OtherUse { get; set; }
        public decimal? LandArea { get; set; }
        public decimal? AreaBuildingFootprint { get; set; }
        public decimal? TotalConstructedArea { get; set; }
        public decimal? VacantArea { get; set; }
        public string HousePropertyNo { get; set; }
        public string PlotNo { get; set; }
        public string Street { get; set; }
        public string NoofFloorConstructed { get; set; }
        public string KhasraNo { get; set; }
        public string LandMark { get; set; }
        public string North { get; set; }
        public string South { get; set; }
        public string East { get; set; }
        public string West { get; set; }
        public string LitigrationStatus { get; set; }
        public string IsHightTensionLine { get; set; }
        public string CourtCaseStatus { get; set; }
        public string CourtCasedetail { get; set; }
        public string CourtName { get; set; }
        public string CaseNo { get; set; }
        public string PetitionerRespondent { get; set; }
        public string OppositeParty { get; set; }
        public string PropertyPhotographFilePath { get; set; }
        public string ElectricityBillFilePath { get; set; }
        public string WaterBillFilePath { get; set; }
        public string PropertyTaxReceiptFilePath { get; set; }
        public int? Declaration1 { get; set; }
        public int? Declaration2 { get; set; }
        public int? Declaration3 { get; set; }
        public byte? IsActive { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public int? ApprovalZoneId { get; set; }
        public string UserId { get; set; }

        //documents
        [NotMapped]
        public IFormFile FilePropertyPhoto { get; set; }
        [NotMapped]
        public IFormFile FileElectricityBill { get; set; }

        [NotMapped]
        public IFormFile FilePropertyTaxReceipt { get; set; }

        [NotMapped]
        public IFormFile FileWaterBill { get; set; }
        //

        //****** Floor Details ******

        [NotMapped]
        public List<int?>  FloorId { get; set; }

        [NotMapped]
        public List<decimal?> CarpetArea { get; set; }

        [NotMapped]
        public List<string> ElectricityKno { get; set; }

        [NotMapped]
        public List<string> McdpropertyTaxId { get; set; }

        [NotMapped]
        public List<string> WaterKno { get; set; }

        [NotMapped]
        public List<string> CurrentUse { get; set; }


        //****** Occupant Details ******

        [NotMapped]
        public List<string> LatestAtsname { get; set; }

        [NotMapped]
        public List<string> LatestGpaname { get; set; }

        [NotMapped]
        public List<string> FirstName { get; set; }

        [NotMapped]
        public List<string> MiddleName { get; set; }

        [NotMapped]
        public List<string> LastName { get; set; }

        [NotMapped]
        public List<string> SpouseName { get; set; }

        [NotMapped]
        public List<string> FatherName { get; set; }

        [NotMapped]
        public List<string> MontherName { get; set; }

        [NotMapped]
        public List<string> Epicid { get; set; }

        [NotMapped]
        public List<string> EmailId { get; set; }

        [NotMapped]
        public List<string> MobileNo { get; set; }

        [NotMapped]
        public List<string> AadharNo { get; set; }

        [NotMapped]
        public List<DateTime?> Dob { get; set; }

        [NotMapped]
        public List<string> Gender { get; set; }

        [NotMapped]
        public List<string> PanNo { get; set; }

        [NotMapped]
        public List<string> ShareInProperty { get; set; }

        [NotMapped]
        public List<string> IsOccupingFloor { get; set; }

        [NotMapped]
        public List<string> FloorNo { get; set; }

        [NotMapped]
        public List<string> DamagePaidInPast { get; set; }

        [NotMapped]
        public List<IFormFile> FileOccupantPhoto { get; set; }

        [NotMapped]
        public List<string> OccupantPhotoPath { get; set; }
       
       

        //****** GPA ******

        [NotMapped]
        public List<DateTime?> DateOfExecutionOfGpa { get; set; }

        [NotMapped]
        public List<string> NameOfTheSeller { get; set; }

        [NotMapped]
        public List<string> NameOfThePayer { get; set; }

        [NotMapped]
        public List<string> AddressOfThePlotAsPerGpa { get; set; }

        [NotMapped]
        public List<string> AreaOfThePlotAsPerGpa { get; set; }
        [NotMapped]
        public List<IFormFile> FileGpafile { get; set; }

        [NotMapped]
        public string GpafilePath { get; set; }


        //****** ATS ******

        [NotMapped]
        public List<DateTime?> DateOfExecutionOfAts { get; set; }

        [NotMapped]
        public List<string> NameOfTheSellerAts { get; set; }

        [NotMapped]
        public List<string> NameOfThePayerAts { get; set; }

        [NotMapped]
        public List<string> AddressOfThePlotAsPerAts { get; set; }

        [NotMapped]
        public List<string> AreaOfThePlotAsPerAts { get; set; }
        [NotMapped]
        public List<IFormFile> FileAtsfile { get; set; }
        [NotMapped]
        public List<string> AtsfilePath { get; set; }


        //****** Holder Details ******

        [NotMapped]
        public List<string> NameOfGpaats { get; set; }

        [NotMapped]
        public List<string> DeathCertificateNo { get; set; }

        [NotMapped]
        public List<DateTime?> DeathCertificateDate { get; set; }

        [NotMapped]
        public List<string> NameOfSurvivingMember { get; set; }

        [NotMapped]
        public List<string> Relationship { get; set; }

        [NotMapped]
        public List<string> IsRelinquished { get; set; }


        //****** Payment History ******

        [NotMapped]
        public List<string> Name { get; set; }

        [NotMapped]
        public List<string> RecieptNo { get; set; }

        [NotMapped]
        public List<string> PaymentMode { get; set; }

        [NotMapped]
        public List<DateTime?> PaymentDate { get; set; }

        [NotMapped]
        public List<decimal?> Amount { get; set; }
        [NotMapped]
        public List<IFormFile> FileRecieptDocument { get; set; }
        [NotMapped]
        public List<string> RecieptDocumentPath { get; set; }




        public Approvalstatus GetApprovedStatusNavigation { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalstatuseList { get; set; }
        public New_Damage_Colony GetColony { get; set; }

        [NotMapped]
        public List<New_Damage_Colony> ColonyList { get; set; }
        public District GetDistrict { get; set; }

        [NotMapped]
        public List<District> districtList { get; set; }

        [NotMapped]
        public Acquiredlandvillage GetVillage { get; set; }

        [NotMapped]
        public List<Floors> floorlist { get; set; }

        //[NotMapped]
        //public List<Village> villageList { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> damagevillageList { get; set; }
        public ICollection<Newdamagepayeeoccupantinfo> Newdamagepayeeoccupantinfo { get; set; }
        public ICollection<Newdamagepaymenthistory> Newdamagepaymenthistory { get; set; }
        public ICollection<Newdamageselfassessmentatsdetail> Newdamageselfassessmentatsdetail { get; set; }
        public ICollection<Newdamageselfassessmentfloordetail> Newdamageselfassessmentfloordetail { get; set; }
        public ICollection<Newdamageselfassessmentgpadetail> Newdamageselfassessmentgpadetail { get; set; }
        public ICollection<Newdamageselfassessmentholderdetail> Newdamageselfassessmentholderdetail { get; set; }

        [NotMapped]
        public bool DeclarationStatus1 { get; set; }
        [NotMapped]
        public bool DeclarationStatus2 { get; set; }
        [NotMapped]
        public bool DeclarationStatus3 { get; set; }
    }
}
