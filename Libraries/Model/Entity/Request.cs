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

        public Request()
        {
            Newlandannexure1 = new HashSet<Newlandannexure1>();
        }

        [Required (ErrorMessage = "Proposal Name is mandatory")]
        public string PproposalName { get; set; }
        [Required(ErrorMessage = "File No is mandatory")]
        public string PfileNo { get; set; }
        [Required(ErrorMessage = "Requiring Body is mandatory")]
        public string RequiringBody { get; set; }
        [Required(ErrorMessage = "Area/Locality is mandatory")]
        public string AreaLocality { get; set; }
      //  [Required(ErrorMessage = "The ProposalName field is required")]
        public string TaunderRequest { get; set; }
        [Required(ErrorMessage = "Unit Area is mandatory")]
        public string UnitArea { get; set; }
        [Required(ErrorMessage = "Purpose of Acquistion is mandatory")]
        public string PurposeOfAcquistion { get; set; }
 
        public string LayoutPlan { get; set; }
       
        public string Remarks { get; set; }
        [Required]
        public byte IsActive { get; set; }

        public int? ApprovedStatus { get; set; }
        public int? PendingAt { get; set; }
        public string ReferenceNo { get; set; }

        [NotMapped]
        //[Required]
        //[DataType(DataType.Upload)] ------ Required Validation for image
        public IFormFile RequestPhotos { get; set; }



        [NotMapped]
        public string ApprovalRemarks { get; set; }
        [NotMapped]
        public string ApprovalStatus { get; set; }


        public ICollection<Newlandannexure1> Newlandannexure1 { get; set; }

    }
}
