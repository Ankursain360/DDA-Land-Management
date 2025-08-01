﻿using System;
using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class ApiInsertVacantLandImageDto
    {
        public int Id { get; set; }
        public int? ZoneId { get; set; }
        public string Zone { get; set; }
        public int? DepartmentId { get; set; }
        public string Department { get; set; }
        public int? DivisionId { get; set; }
        public string Division { get; set; }
        public int? PrimaryListId { get; set; }
        public string PrimaryList { get; set; }
        public string Location { get; set; }
        public IList<string> ImageData { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string SrNoInPrimaryList { get; set; }
        public string Flag { get; set; }
        public string Mobile { get; set; }
        public int? CheckingPoint { get; set; }
        public string BoundaryWall { get; set; }
        public string Fencing { get; set; }
        public string Ddaboard { get; set; }
        public string ScurityGuard { get; set; }
        public int? UniqueId { get; set; }
        public string CertifiedPlot { get; set; } 
        public string EncroachmentDetails { get; set; }
        public string IsEncroached { get; set; }
        public string percentageEncroached { get; set; } 
        public string AreaEncroached { get; set; }
        public string IsActionInitiated { get; set; }
        public string Remarks { get; set; }
        public int createdby { get; set; } 

    }

    public class ApiInsertVacantLandImageResponseDetails
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<ApiInsertVacantLandImageDto> response { get; set; }
    }
}
