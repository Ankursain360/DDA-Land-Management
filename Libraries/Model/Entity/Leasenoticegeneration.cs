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
    public class Leasenoticegeneration : AuditableEntity<int>
    {
        public int RequestProceedingId { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public string MeetingPlace { get; set; }
        public string NoticeFileName { get; set; }
        public byte? IsActive { get; set; }

        public Requestforproceeding RequestProceeding { get; set; }

        [NotMapped]
        public IFormFile Document { get; set; }

        [NotMapped]
        public int GenerateUpload { get; set; }
    }
}
