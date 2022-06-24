using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libraries.Model.Entity
{
    public partial class SelfAssessmentForm : AuditableEntity<int>
    {
        public SelfAssessmentForm()
        {
        }
        //COLONY DETAILS
        public string IsDamagePayee { get; set; }
        public string RegNo { get; set; }
        public int? DistrictId { get; set; }
        public string Village { get; set; }
        public int? LocalityId { get; set; }
        public string LatestATSName { get; set; }
        public string LatestGPAName { get; set; }
        public string PIN { get; set; }

        //PROPERTY SCHEDULE
        public string North { get; set; }
        public string South { get; set; }
        public string East { get; set; }
        public string West { get; set; }
       

        //PROPERTY DETAILS
        public string  PropertyType { get; set; }
        public string ConstructedArea { get; set; }
        public string HouseNo { get; set; }
        public string PlotNo { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string Floor { get; set; }
        public string FootPrint { get; set; }
        public string Year { get; set; }
        public string Width { get; set; }

        //FLOOR DETAILS
        public string FloorName { get; set; }
        public string CarpetArea { get; set; }
        public string ElectricityNumber { get; set; }
        public string MCDPropertyTaxID { get; set; }
        public string WaterBill { get; set; }
        public string CurrentUse { get; set; }


        //OWNERSHIP DETAILS
        public string OwnerFirstName { get; set; }
        public string OwnerMiddleName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerSpouseName { get; set; }
        public string OwnerFatherName { get; set; }
        public string OwnerMotherName { get; set; }
        public string EPICIDNumber { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string AADHAAR { get; set; }
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public string PAN { get; set; }
        public string Colony { get; set; }
        public string District { get; set; }
        public string ShareinProperty { get; set; }
        public string OwnerPhoto { get; set; }


        //LAND DETAILS
        public string LandFallLitigation { get; set; }
        public string LitigationStatus { get; set; }
        public string CourtCaseStatus { get; set; }
        public string CourtCaseDetails { get; set; }
        public string CourtName { get; set; }
        public string CaseNumber { get; set; }
        public string Petitioner { get; set; }
        public string OppositeParty { get; set; }

        //DETAILSOFDOCUMENTS
        public string PropertyPhoto { get; set; }
        public string VerifyChainDocument { get; set; }
        public string GPADocument { get; set; }
        public string ATSDocument { get; set; }
        public string ElectricityBill { get; set; }
        public string PaymentDocument { get; set; }
        public string WillDocument { get; set; }
        public string PossessionDocument { get; set; }
        public string PropertyTaxMutationDocument { get; set; }
        public string CoOrdinate { get; set; }

        //GPADETAIL
        public string ExecutionDateGPA { get; set; }
        public string SellerNameGPA { get; set; }
        public string PayerNameGPA { get; set; }
        public string PlotAddressGPA { get; set; }
        public string PlotAreaGPA { get; set; }

        //ATSDETAIL
        public string ExecutionDateATS { get; set; }
        public string SellerNameATS { get; set; }
        public string PayerNameATS { get; set; }
        public string PlotAddressATS { get; set; }
        public string PlotAreaATS { get; set; }

        //Declaration
        public string DeclarationStatus1 { get; set; }
        public string Declaration1 { get; set; }
        public string DeclarationStatus2 { get; set; }
        public string Declaration2 { get; set; }
        public string DeclarationStatus3 { get; set; }
        public string Declaration3 { get; set; }

        public List<District> DistrictList { get; set; }
        public List<Locality> LocalityList { get; set; }
    }
}
