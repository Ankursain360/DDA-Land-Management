using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class NewDamageSelfAssessment : AuditableEntity<int>
    {
        public NewDamageSelfAssessment()
        {
            NewdamageAddfloor = new HashSet<NewdamageAddfloor>();
            NewdamageSelfassessmentAtsdetail = new HashSet<NewDamageSelfAssessmentAtsDetails>();
            NewdamageSelfassessmentGpadetail = new HashSet<NewDamageSelfAssessmentGpaDetails>();
        }

        public string RegId { get; set; }
        public int Districtid { get; set; }
        public int VillageId { get; set; }
        public int ColonyId { get; set; }
        public string Latestatsname { get; set; }

       // public string Latestatsname { get; set; }
        public string Pin { get; set; }
        public string North { get; set; }
        public string South { get; set; }
        public string East { get; set; }
        public string West { get; set; }
        public string TypeProperty { get; set; }
        public decimal? ConstructedArea { get; set; }
        public string HouseNo { get; set; }
        public string PlotNo { get; set; }
        public string Street { get; set; }
        public int LocalityId { get; set; }
        public int? NosFloor { get; set; }
        public decimal? BuildingFootprintArea { get; set; }
        public int? ConstructionYear { get; set; }
        public decimal? FrontRoadWidth { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SpouseName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EpicIdNumber { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AadhaarNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PanNo { get; set; }
        public string OwnershipColony { get; set; }
        public string OwnershipDistrictId { get; set; }
        public string PropertyShare { get; set; }
        public string OwnerPhoto { get; set; }
        public string DoesLandLitigation { get; set; }
        public string LitigationStatus { get; set; }
        public string CourtCaseStatus { get; set; }
        public string DetailCourtCase { get; set; }
        public string CourtName { get; set; }
        public string CaseNumber { get; set; }
        public string PetitionerRespondent { get; set; }
        public string NameOppositeParty { get; set; }
        public string PhotographProperty { get; set; }
        public string PhotographOwner { get; set; }
        public string Gpa { get; set; }
        public string Ats { get; set; }
        public string ElectricityBill { get; set; }
        public string PaymentDocument { get; set; }
        public string WillDocument { get; set; }
        public string PossessionDocument { get; set; }
        public string MutationDocument { get; set; }
        public string CoordinateDocument { get; set; }
        public string Declaration1 { get; set; }
        public string Declaration2 { get; set; }
        public string Declaration3 { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public int? RecordStatus { get; set; }
        public string ChainDocument { get; set; }

        [NotMapped]
        public IFormFile PhotographarFP { get; set; }

        [NotMapped]
        public IFormFile PhotographOwnerFP { get; set; }

        [NotMapped]
        public IFormFile GpaFilePathFP { get; set; }

        [NotMapped]
        public IFormFile AtsFilePathFP { get; set; }

        [NotMapped]
        public IFormFile ElectricityBillFP { get; set; }
        [NotMapped]
        public IFormFile PaymentDocumentFP { get; set; }
        [NotMapped]
        public IFormFile WillDocumentFP { get; set; }
        [NotMapped]
        public IFormFile NewDamagePayeeAssmtFP { get; set; }

        [NotMapped]
        public IFormFile PossessionDocumentFP { get; set; }

        [NotMapped]
        public IFormFile MutationDocumentFP { get; set; }

        [NotMapped]
        public IFormFile CoordinateDocumentFP { get; set; }

        [NotMapped]
        public IFormFile ChainDocumentFP { get; set; }

        

        [NotMapped]
        public List<IFormFile> Document { get; set; }


        //***** Add Floor *****

        [NotMapped]
        public int NewDamageSelfAssessmentId { get; set; }

        [NotMapped]
        public string FloorName { get; set; }

        [NotMapped]
        public decimal? CarpetArea { get; set; }

        [NotMapped]
        public string ElectricityNumber { get; set; }

        [NotMapped]
        public string MuncipleTaxId { get; set; }

        [NotMapped]
        public string WaterBill { get; set; }

        [NotMapped]
        public string CurrentUse { get; set; }

        [NotMapped]
        public string Status { get; set; }

        [NotMapped]
        public List<string> AFloorName { get; set; }

        [NotMapped]
        public List<string> ACarpetArea { get; set; }

        [NotMapped]
        public List<string> AElectricityNumber { get; set; }
        [NotMapped]

        public List<string> AWaterBill { get; set; }
        [NotMapped]

        public List<string> ACurrentUse { get; set; }
        [NotMapped]

        public List<string> AMuncipleTaxId { get; set; }

        //***** GPA *****
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

        [NotMapped]
        public List<DateTime> ADateOfExecutionOfGpa { get; set; }

        [NotMapped]
        public List<String> ANameOfTheSeller { get; set; }

        [NotMapped]
        public List<String> ANameOfThePayer { get; set; }

        [NotMapped]
        public List<String> AAddressOfThePlotAsPerGpa { get; set; }

        [NotMapped]
        public List<String> AAreaOfThePlotAsPerGpa { get; set; }

        //***** ATS *****

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



        [NotMapped]
        public List<DateTime> ADateOfExecutionOfAts { get; set; }

        [NotMapped]
        public List<String> ANameOfTheSellerAts { get; set; }

        [NotMapped]
        public List<String> ANameOfThePayerAts { get; set; }

        [NotMapped]
        public List<String> AAddressOfThePlotAsPerAts { get; set; }

        [NotMapped]
        public List<String> AAreaOfThePlotAsPerAts { get; set; }





        [NotMapped]
        public List<New_Damage_Colony> New_DamageColonyList { get; set; }
        public New_Damage_Colony GetNew_Damage_Colony { get; set; }

        [NotMapped]
        public List<District> DistrictList { get; set; }
        public District GetDistrict { get; set; }

        [NotMapped]
        public List<Acquiredlandvillage> AcquiredlandvillageList { get; set; }
        public Acquiredlandvillage GetAcquiredLandVillage { get; set; }

        [NotMapped]
        public List<Locality> LocalitieList { get; set; }
        [NotMapped]
        public List<New_Damage_Colony> Colonylist { get; set; }
        public Locality GetLocality { get; set; }

        public ICollection<NewdamageAddfloor> NewdamageAddfloor { get; set; }
        public ICollection<NewDamageSelfAssessmentAtsDetails> NewdamageSelfassessmentAtsdetail { get; set; }
        public ICollection<NewDamageSelfAssessmentGpaDetails> NewdamageSelfassessmentGpadetail { get; set; }
    }
}
