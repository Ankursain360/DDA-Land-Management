using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Demolitionpoliceassistenceletter : AuditableEntity<int>
    {
      
        public int FixingDemolitionId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeDepartment { get; set; }
        public string OfficeZone { get; set; }
        public string OfficeAddress { get; set; }
        public string FileNo { get; set; }
        public DateTime? LetterDate { get; set; }
        public string AuthorityDesignation { get; set; }
        public string DyCommOffcAddress { get; set; }
        public string KhasraNo { get; set; }
        public string VillageName { get; set; }
        public string KhasraAddress { get; set; }
        [Required(ErrorMessage = "Meeting Date is mandatory field")]
        public DateTime? MeetingDate { get; set; }

        [Required(ErrorMessage = "Meeting Time is mandatory field")]
        public string MeetingTime { get; set; }
        public string GeneralConditions { get; set; }
        public string ChiefEngineerAddress { get; set; }
        public string Shoaddress { get; set; }

        [Required(ErrorMessage = "Police Station Name is mandatory field")]
        public string PoliceStationName { get; set; }
        public DateTime? OperationDate { get; set; }
        public string OperationDay { get; set; }
        public string RevenueOfficerZone { get; set; }
        public string RevenueOfficerWing { get; set; }
        public string RevenueOfficerBranch { get; set; }
      
        public byte? IsActive { get; set; }

        public Fixingdemolition FixingDemolition { get; set; }

        //[NotMapped]
        //public IFormFile Document { get; set; }

        [NotMapped]
        public int GenerateUpload { get; set; }
    }
}
