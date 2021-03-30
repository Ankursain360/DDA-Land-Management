using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{

      public class Hearingdetails : AuditableEntity<int>
    {
        public Hearingdetails()
        {
            Hearingdetailsphotofiledetails = new HashSet<Hearingdetailsphotofiledetails>();
        }

        public int ReqProcId { get; set; }
        public int NoticeGenId { get; set; }
        public int EvidanceDocId { get; set; }
        public DateTime? HearingDate { get; set; }
        public TimeSpan? HearingTime { get; set; }
        public string HearingVenue { get; set; }
        public string Attendee { get; set; }
        public string Remark { get; set; }
        public byte IsActive { get; set; }
       
        public Evidancedoc EvidanceDoc { get; set; }
        public Leasenoticegeneration NoticeGen { get; set; }
        public Requestforproceeding ReqProc { get; set; }
        [NotMapped]
        public List<IFormFile> Photo { get; set; }
        public virtual ICollection<Hearingdetailsphotofiledetails> Hearingdetailsphotofiledetails { get; set; }
        [NotMapped]
        public string Latitude { get; set; }

        [NotMapped]
        public string Longitude { get; set; }
    }
}
