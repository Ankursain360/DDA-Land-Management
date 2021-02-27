using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Http;


namespace Libraries.Model.Entity
{
  public  class Request : AuditableEntity<int>
    {


     [Required]
        public string PproposalName { get; set; }
        [Required]
        public string PfileNo { get; set; }
        [Required]
        public string RequiringBody { get; set; }
        [Required]
        public string AreaLocality { get; set; }
        [Required]
        public string TaunderRequest { get; set; }
        [Required]
        public string UnitArea { get; set; }
        [Required]
        public string PurposeOfAcquistion { get; set; }
 
        public string LayoutPlan { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public byte IsActive { get; set; }

        public int? ApprovedStatus { get; set; }
        public int? PendingAt { get; set; }



        [NotMapped]
        public IFormFile RequestPhotos { get; set; }



        [NotMapped]
        public string ApprovalRemarks { get; set; }
        [NotMapped]
        public string ApprovalStatus { get; set; }




    }
}
