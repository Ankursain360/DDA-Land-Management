
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiSaveWatchandwardDto
    {
        public int Id { get; set; }
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
       // public string LocalityName { get; set; }
       // public string KhasraName { get; set; }
    }

    public class ApiSaveWatchandwardDtoResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiSaveWatchandwardDto> ApiSaveWatchandwardDto { get; set; }
    }
}
