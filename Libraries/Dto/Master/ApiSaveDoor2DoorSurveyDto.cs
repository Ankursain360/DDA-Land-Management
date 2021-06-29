using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class ApiSaveDoor2DoorSurveyDto
    {
        public int  Id { get; set; }
        public string PropertyAddress { get; set; }
        public string GeoReferencingLattitude { get; set; }
        public string Longitude { get; set; }
        public int? PresentUseId { get; set; }
        public decimal? ApproxPropertyArea { get; set; }
        public string NumberOfFloors { get; set; }

        public string CaelectricityNo { get; set; }

        public string KwaterNo { get; set; }

        public string PropertyHouseTaxNo { get; set; }
        public string OccupantName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string OccupantAadharNo { get; set; }
        public string VoterIdNo { get; set; }

        public string OccupantIdentityPrrofFilePath { get; set; }

        public string DamagePaidPast { get; set; }
        public string PropertyFilePath { get; set; }
        public string Remarks { get; set; }

        public byte IsActive { get; set; }
        public string OccupantIdentityPrrofFileData { get; set; }
        public string PropertyFileData { get; set; }
        public int CreatedBy { get; set; }
        public string PresentUseName { get; set; }


    }

    public class ApiSaveDoor2DoorSurveyResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiSaveDoor2DoorSurveyDto> ApiSaveDoor2DoorSurveyDto { get; set; }
    }
}
