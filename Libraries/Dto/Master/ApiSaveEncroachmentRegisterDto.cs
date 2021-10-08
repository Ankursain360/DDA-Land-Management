
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//using Libraries.Model.Entity;
namespace Dto.Master
{
    public class ApiSaveEncroachmentRegisterDto
    {
        public int Id { get; set; }
        public int? WatchWardId { get; set; }
        public int UserId { get; set; }
        public string RefNo { get; set; }
        
        public int DepartmentId { get; set; }
        public int ZoneId { get; set; }
        public int DivisionId { get; set; }
        public int LocalityId { get; set; }
        
        public DateTime EncrochmentDate { get; set; }
        public string KhasraNo  { get; set; }
        public int AreaUnit { get; set; }
        public int? TotalAreaInBighaInspection { get; set; }
        public int? TotalAreaInBiswaInspection { get; set; }
        public int? TotalAreaInBiswaniInspection { get; set; }
        public decimal? TotalAreaInSqAcreHt { get; set; }
        public decimal Area { get; set; }
        public string LocationAddressWithLandMark { get; set; }
        public string EncroacherName { get; set; }
        public string StatusOfLand { get; set; }
         public string IsPossession { get; set; }
        public string PossessionType { get; set; }
        public int? OtherDepartment { get; set; }
        public string PoliceStation { get; set; }
        public string SecurityGuardOnDuty { get; set; }
        public string IsEncroachment { get; set; }
       
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
       // public int CreatedBy { get; set; }
        
       
        public IList<string> PhotoFileData { get; set; }
        //public IList<string> ReportFileData { get; set; }
        // public List<IFormFile> ReportFileData { get; set; }
        //public IList<string> PhotoFileDataPath { get; set; }
        //public IList<string> ReportFileDataPath { get; set; }
        
        public int? ApprovalZoneId { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        // public string LocalityName { get; set; }
        // public string KhasraName { get; set; }

        #region DetailsOfEncroachment table feilds
        public List<string> NameOfStructure { get; set; }
        public List<decimal> AreaApprox { get; set; }
        public List<string> Type { get; set; }
        public List<int> DateOfEncroachment { get; set; }
        public List<decimal> CountOfStructure { get; set; }
        public List<string> ReferenceNoOnLocation { get; set; }
        public List<string> ConstructionStatus { get; set; }
        public List<string> ReligiousStructure { get; set; }

        #endregion

        #region EncroachmentFirFileDetails table fields
        public List<IFormFile> Firfile { get; set; }
        #endregion

        #region  EncroachmentPhotoFileDetails table fields
        public List<IFormFile> PhotoFile { get; set; }
        #endregion
        #region  EncroachmentLocationMapFileDetails table fields
        public List<IFormFile> LocationMapFile { get; set; }
        #endregion

        #region Approval Related Fields
        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public int ApprovalStatusCode { get; set; }

       

        [NotMapped]
        public int ApprovalUserId { get; set; }

        [NotMapped]
        public int ApprovalRoleId { get; set; }

        #endregion
    }

    public class ApiSaveEncroachmentRegisterDtoResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiSaveEncroachmentRegisterDto> ApiSaveEncroachmentRegisterDto { get; set; }
    }
}
