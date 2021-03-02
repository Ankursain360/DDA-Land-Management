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


     [Required (ErrorMessage = "The Proposal Name field is required")]
        public string PproposalName { get; set; }
        [Required(ErrorMessage = "The File No field is required")]
        public string PfileNo { get; set; }
        [Required(ErrorMessage = "The Requiring Body field is required")]
        public string RequiringBody { get; set; }
        [Required(ErrorMessage = "The Area/Locality field is required")]
        public string AreaLocality { get; set; }
      //  [Required(ErrorMessage = "The ProposalName field is required")]
        public string TaunderRequest { get; set; }
        [Required(ErrorMessage = "The Unit Area field is required")]
        public string UnitArea { get; set; }
        [Required(ErrorMessage = "The Purpose of Acquistion field is required")]
        public string PurposeOfAcquistion { get; set; }
 
        public string LayoutPlan { get; set; }
       
        public string Remarks { get; set; }
        [Required]
        public byte IsActive { get; set; }

        public int? ApprovedStatus { get; set; }
        public int? PendingAt { get; set; }
        public string ReferenceNo { get; set; }

        [NotMapped]
    //    [Required(ErrorMessage = "The Layout Plan field is required")]
    //    [DataType(DataType.Upload)]
        public IFormFile RequestPhotos { get; set; }



        [NotMapped]
        public string ApprovalRemarks { get; set; }
        [NotMapped]
        public string ApprovalStatus { get; set; }




    }
}
