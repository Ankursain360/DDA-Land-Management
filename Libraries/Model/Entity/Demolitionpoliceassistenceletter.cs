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

        [Required(ErrorMessage = "Meeting Date is mandatory field")]
        public DateTime? MeetingDate { get; set; }

        [Required(ErrorMessage = "Meeting Time is mandatory field")]
        public string MeetingTime { get; set; }

        [Required(ErrorMessage = "Police Station Name is mandatory field")]
        public string PoliceStationName { get; set; }

        [Required(ErrorMessage = "Address/KhasraNo is mandatory field")]
        public string AddressKhasraNo { get; set; }

        [Required(ErrorMessage = "Religious is mandatory field")]
        public string IsReligiousStructure { get; set; }
        public string FilePath { get; set; }
        public Fixingdemolition FixingDemolition { get; set; }

        [NotMapped]
        public IFormFile Document { get; set; }

        [NotMapped]
        public int GenerateUpload { get; set; }
    }
}
