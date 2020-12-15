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
        public DateTime? MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public string FilePath { get; set; }
        public Fixingdemolition FixingDemolition { get; set; }

        [NotMapped]
        public IFormFile Document { get; set; }

        [NotMapped]
        public int GenerateUpload { get; set; }
    }
}
