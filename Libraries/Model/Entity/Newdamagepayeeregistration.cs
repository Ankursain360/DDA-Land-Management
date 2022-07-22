using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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


        public string WhetherDamageProp { get; set; }
        public string Occupanttype { get; set; }
        public string IsNameChanged { get; set; }
        public string FileNo { get; set; }
        public int? DistrictId { get; set; }
        public int? VillageId { get; set; }
        public int? ColonyId { get; set; }
        public string OtherColonyName { get; set; }
        public string PinCode { get; set; }
        public string TypeOfProperty { get; set; }
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


        //****** Floor Details ******

        [NotMapped]
        public int? FloorId { get; set; }

        [NotMapped]
        public decimal? CarpetArea { get; set; }

        [NotMapped]
        public string ElectricityKno { get; set; }

        [NotMapped]
        public string McdpropertyTaxId { get; set; }

        [NotMapped]
        public string WaterKno { get; set; }

        [NotMapped]
        public string CurrentUse { get; set; }


        //****** Occupant Details ******

        [NotMapped]
        public string LatestAtsname { get; set; }

        [NotMapped]
        public string LatestGpaname { get; set; }

        [NotMapped]
        public string FirstName { get; set; }

        [NotMapped]
        public string MiddleName { get; set; }

        [NotMapped]
        public string LastName { get; set; }

        [NotMapped]
        public string SpouseName { get; set; }

        [NotMapped]
        public string FatherName { get; set; }

        [NotMapped]
        public string MontherName { get; set; }

        [NotMapped]
        public string Epicid { get; set; }

        [NotMapped]
        public string EmailId { get; set; }

        [NotMapped]
        public string MobileNo { get; set; }

        [NotMapped]
        public string AadharNo { get; set; }

        [NotMapped]
        public DateTime? Dob { get; set; }

        [NotMapped]
        public string Gender { get; set; }

        [NotMapped]
        public string PanNo { get; set; }

        [NotMapped]
        public string ShareInProperty { get; set; }

        [NotMapped]
        public string IsOccupingFloor { get; set; }

        [NotMapped]
        public string FloorNo { get; set; }

        [NotMapped]
        public string DamagePaidInPast { get; set; }

        [NotMapped]
        public string OccupantPhotoPath { get; set; }

        [NotMapped]
        public string GpafilePath { get; set; }

        [NotMapped]
        public string AtsfilePath { get; set; }

        //****** GPA ******

        [NotMapped]
        public DateTime? DateOfExecutionOfGpa { get; set; }

        [NotMapped]
        public string NameOfTheSeller { get; set; }

        [NotMapped]
        public string NameOfThePayer { get; set; }

        [NotMapped]
        public string AddressOfThePlotAsPerGpa { get; set; }

        [NotMapped]
        public string AreaOfThePlotAsPerGpa { get; set; }


        //****** ATS ******

        [NotMapped]
        public DateTime? DateOfExecutionOfAts { get; set; }

        [NotMapped]
        public string NameOfTheSellerAts { get; set; }

        [NotMapped]
        public string NameOfThePayerAts { get; set; }

        [NotMapped]
        public string AddressOfThePlotAsPerAts { get; set; }

        [NotMapped]
        public string AreaOfThePlotAsPerAts { get; set; }


        //****** Holder Details ******

        [NotMapped]
        public string NameOfGpaats { get; set; }

        [NotMapped]
        public string DeathCertificateNo { get; set; }

        [NotMapped]
        public DateTime? DeathCertificateDate { get; set; }

        [NotMapped]
        public string NameOfSurvivingMember { get; set; }

        [NotMapped]
        public string Relationship { get; set; }

        [NotMapped]
        public string IsRelinquished { get; set; }


        //****** Payment History ******

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string RecieptNo { get; set; }

        [NotMapped]
        public string PaymentMode { get; set; }

        [NotMapped]
        public DateTime? PaymentDate { get; set; }

        [NotMapped]
        public decimal? Amount { get; set; }

        [NotMapped]
        public string RecieptDocumentPath { get; set; }




        public Approvalstatus GetApprovedStatusNavigation { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalstatuseList { get; set; }
        public New_Damage_Colony GetColony { get; set; } 

        [NotMapped]
        public List<New_Damage_Colony> ColonyList { get; set; }
        public District GetDistrict { get; set; }
         
        [NotMapped]
        public List<District> districtList { get; set; }
        public Village GetVillage { get; set; } 

        [NotMapped]
        public List<Village> villageList { get; set; } 
        public ICollection<Newdamagepayeeoccupantinfo> Newdamagepayeeoccupantinfo { get; set; }
        public ICollection<Newdamagepaymenthistory> Newdamagepaymenthistory { get; set; }
        public ICollection<Newdamageselfassessmentatsdetail> Newdamageselfassessmentatsdetail { get; set; }
        public ICollection<Newdamageselfassessmentfloordetail> Newdamageselfassessmentfloordetail { get; set; }
        public ICollection<Newdamageselfassessmentgpadetail> Newdamageselfassessmentgpadetail { get; set; }
        public ICollection<Newdamageselfassessmentholderdetail> Newdamageselfassessmentholderdetail { get; set; }
    }
}
