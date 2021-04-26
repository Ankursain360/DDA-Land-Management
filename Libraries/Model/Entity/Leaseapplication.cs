using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Libraries.Model.Entity
{
    public class Leaseapplication : AuditableEntity<int>
    {
        public Leaseapplication()
        {
            Allotmententry = new HashSet<Allotmententry>();
            Leaseapplicationdocuments = new HashSet<Leaseapplicationdocuments>();
            Mortgage = new HashSet<Mortgage>();
            Extensionservice = new HashSet<Extension>();
        }
        public string RefNo { get; set; }

        [Required(ErrorMessage = "Name is Mandatory")]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile No/Telephone No. is Mandatory")]
        public string ContactNo { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "EmailId is not valid.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Registeration No. is Mandatory")]
        public string RegistrationNo { get; set; }
        public string Description { get; set; }
        public string LandPurpose { get; set; }
        public string LandDetailsArea { get; set; }
        public string IncomeTaxDescription { get; set; }
        public DateTime? SponsorshipDate { get; set; }
        public string SponsorshipDescription { get; set; }
        public string RecommendationDescription { get; set; }
        public decimal? LandAreaSqMt { get; set; }
        public string NotarizedUndertakingDescription { get; set; }
        public string IndemnityDescription { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string LandAuthorisingDescription { get; set; }
        public string FinancialPositionDescription { get; set; }
        public string ProposedDescription { get; set; }
        public string EstablishmentNameAddress { get; set; }
        public string FunctioningSinceWhen { get; set; }
        public string FunctioningActivityUndertaken { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public decimal? FunctioningAreaSqMt { get; set; }
        public string RefNoOfAllotmentLetterDate { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public decimal? AreaSqlMt { get; set; }
        public string Locality { get; set; }
        public string Purpose { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid; Max 18 digits")]
        public decimal? Rate { get; set; }
        public byte IsActive { get; set; }
        public int? ApprovalZoneId { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public int? UserId { get; set; }
        public ICollection<Leaseapplicationdocuments> Leaseapplicationdocuments { get; set; }
        public ICollection<Allotmententry> Allotmententry { get; set; }
        //****** Document repeator *****

        [NotMapped]
        public List<string> DocumentName { get; set; }

        [NotMapped]
        public List<string> Mandatory { get; set; }

        [NotMapped]
        public List<int> IsMandatory { get; set; }

        [NotMapped]
        public List<int> DocumentChecklistId { get; set; }

        [NotMapped]
        public List<int> ServiceId { get; set; }

        [NotMapped]
        public List<IFormFile> FileUploaded { get; set; }
        [NotMapped]
        public List<string> FileUploadedPath { get; set; }


        [NotMapped]
        public List<Documentchecklist> Documentchecklist { get; set; }

        [NotMapped]
        public List<Leaseapplicationdocuments> Leasedocuments { get; set; }

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

        // Old Allotment entryfeilds

        [NotMapped]
        public decimal TotalArea { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Allotment Date is Mandatory")]
        public DateTime AllotmentDate { get; set; }
        [NotMapped]
        public string PlotNo { get; set; }
        [NotMapped]
        public DateTime? PossessionTakenDate { get; set; }
        [NotMapped]
        public decimal? BuildingArea { get; set; }

        [NotMapped]
        public decimal PlayGroundArea { get; set; }
        [NotMapped]
        public decimal? PremiumAmount { get; set; }
        [NotMapped]
        public decimal? AmountLicFee { get; set; }
        [NotMapped]
        public int? NoOfYears { get; set; }
        [NotMapped]
        public decimal? GroundRate { get; set; }

        [NotMapped]
        public int PropertyTypeId { get; set; }
        [NotMapped]
        public List<PropertyType> PropertyTypeList { get; set; }
        [NotMapped]
        public int LeaseTypeId { get; set; }
        [NotMapped]
        public List<Leasetype> LeaseTypeList { get; set; }
        [NotMapped]
        public int? PurposeId { get; set; }
        [NotMapped]
        public List<Leasepurpose> LeasePurposeList { get; set; }
        [NotMapped]
        public int SubPurposeId { get; set; }
        [NotMapped]
        public List<Leasesubpurpose> LeaseSubPurposeList { get; set; }
        [NotMapped]
        public List<Allotmententry> RefNoList { get; set; }
        public ICollection<Mortgage> Mortgage { get; set; }
        public ICollection<Extension> Extensionservice { get; set; }

        public Approvalstatus ApprovedStatusNavigation { get; set; }
    }
}
