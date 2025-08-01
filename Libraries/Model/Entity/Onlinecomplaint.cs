﻿using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Libraries.Model.Entity
{
    public class Onlinecomplaint : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Complainant Name Is Mandatory")]
        public string Name { get; set; }

        public string EncroacherName { get; set; }
        public string DDALand { get; set; }

        [Required(ErrorMessage = "Contact No Is Mandatory ")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Invalid Contact No. ")]
        public string Contact { get; set; }
       
        public int? ComplaintTypeId { get; set; }
        [Required(ErrorMessage = " This Field Is Mandatory")]

        public string AddressOfComplaint { get; set; }
        [Required(ErrorMessage = "Location Is Mandatory", AllowEmptyStrings = false)]
     
        public int? LocationId { get; set; }
        [Required(ErrorMessage = "Lattitude Is Mandatory ")]
        public string Lattitude { get; set; }
        [Required(ErrorMessage = "Longitude Is Mandatory ")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = " Address/Landmark of Encroached Property Is Mandatory")]
        public string AddressOfEncroachedProperty { get; set; }
        public byte? IsActive { get; set; }

     
        public string PhotoPath { get; set; }
        [Required(ErrorMessage = "Email Is Mandatory ")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        public string ReferenceNo { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public int? ApprovalZoneId { get; set; }

        public string Remarks { get; set; }


        [NotMapped]
        public List<ComplaintType> ComplaintList { get; set; }
        public virtual ComplaintType ComplaintType { get; set; }



        [NotMapped]
        public List<Location> LocationList { get; set; }
        public virtual Location Location { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Photograph Is Mandatory ")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
        public Approvalstatus ApprovedStatusNavigation { get; set; }
        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public int ApprovalStatusCode { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }

        [NotMapped]
        public IFormFile ApprovalDocument { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalStatusList { get; set; }

        [NotMapped]
        public int ApprovalUserId { get; set; }

        [NotMapped]
        public int ApprovalRoleId { get; set; }



    }
}
