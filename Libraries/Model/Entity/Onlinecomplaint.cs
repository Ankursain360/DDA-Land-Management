using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Libraries.Model.Entity
{
    public class Onlinecomplaint : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter   Mobile No ")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Please Select Complaint Type ")]
        public int? ComplaintTypeId { get; set; }
        [Required(ErrorMessage = "Please enter Address ")]
        public string AddressOfComplaint { get; set; }
        [Required(ErrorMessage = "Please Select Location")]
        public int? LocationId { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public byte? IsActive { get; set; }

    
        public string PhotoPath { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        public string ReferenceNo { get; set; }
        public int? ApprovedStatus { get; set; }
        public int? PendingAt { get; set; }
        [Required(ErrorMessage = "The Remarks is required")]
        public string Remarks { get; set; }


        [NotMapped]
        public List<ComplaintType> ComplaintList { get; set; }
        public virtual ComplaintType ComplaintType { get; set; }



        [NotMapped]
        public List<Location> LocationList { get; set; }
        public virtual Location Location { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }
        [NotMapped]
        public string ApprovalStatus { get; set; }


    }
}
