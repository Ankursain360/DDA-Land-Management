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
        [Required(ErrorMessage = " Hearing Date is mandatory")]
        public DateTime? HearingDate { get; set; }
        [Required(ErrorMessage = " Hearing Time is mandatory")]
        public TimeSpan? HearingTime { get; set; }
        [Required(ErrorMessage = " Hearing Venue is mandatory")]
        public string HearingVenue { get; set; }
        public string Attendee { get; set; }
        public string Remark { get; set; }
        public byte IsActive { get; set; }
       public string DocumentPatth { get; set; }

        public Requestforproceeding ReqProc { get; set; }
       

        
        [NotMapped]
        public IFormFile Photo { get; set; }
        public virtual ICollection<Hearingdetailsphotofiledetails> Hearingdetailsphotofiledetails { get; set; }
        [NotMapped]
        public string Latitude { get; set; }

        [NotMapped]
        public string Longitude { get; set; }
    }
}
