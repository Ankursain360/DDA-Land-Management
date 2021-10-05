
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//using Libraries.Model.Entity;
namespace Dto.Master
{
    public class ApiSaveWatchandwardDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefNo { get; set; }
        public DateTime? Date { get; set; }
       // public int? LocalityId { get; set; }
        //public int? KhasraId { get; set; }
        public int PrimaryListNo { get; set; }
        public string Landmark { get; set; }
        public int Encroachment { get; set; }
        public string StatusOnGround { get; set; }
        public string PhotoPath { get; set; }
       // public string ReportFilePath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public IList<string> PhotoFileData { get; set; }
        //public IList<string> ReportFileData { get; set; }
        // public List<IFormFile> ReportFileData { get; set; }
        //public IList<string> PhotoFileDataPath { get; set; }
        //public IList<string> ReportFileDataPath { get; set; }
        public string PrimaryListNoName { get; set; }
         public int? ApprovalZoneId { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        // public string LocalityName { get; set; }
        // public string KhasraName { get; set; }

        #region Approval Related Fields
        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public int ApprovalStatusCode { get; set; }

        //[NotMapped]
        //public string ApprovalRemarks { get; set; }

        //[NotMapped]
        //public IFormFile ApprovalDocument { get; set; }

        //[NotMapped]
        //public List<Approvalstatus> ApprovalStatusList { get; set; }

        [NotMapped]
        public int ApprovalUserId { get; set; }

        [NotMapped]
        public int ApprovalRoleId { get; set; }

        #endregion
    }

    public class ApiSaveWatchandwardDtoResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiSaveWatchandwardDto> ApiSaveWatchandwardDto { get; set; }
    }
}
