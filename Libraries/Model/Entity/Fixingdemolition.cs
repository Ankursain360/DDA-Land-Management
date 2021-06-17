using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using Microsoft.AspNetCore.Http;
namespace Libraries.Model.Entity
{
    public partial class Fixingdemolition : AuditableEntity<int>
    {

        public Fixingdemolition()
        {
            Fixingchecklist = new HashSet<Fixingchecklist>();
            Fixingprogram = new HashSet<Fixingprogram>();
            Fixingdocument = new HashSet<Fixingdocument>();
            Demolitionpoliceassistenceletter = new HashSet<Demolitionpoliceassistenceletter>();
            Demolitionstructuredetails = new HashSet<Demolitionstructuredetails>();
        }
        public string RefNo { get; set; }


        public string DemolitionUniqueId { get; set; }
        public int EncroachmentId { get; set; }
        public byte IsActive { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public int? ApprovalZoneId { get; set; }

        public Approvalstatus ApprovedStatusNavigation { get; set; }

        [NotMapped]
        public List<Demolitionchecklist> Demolitionchecklist { get; set; }
        [NotMapped]
        public List<Demolitionprogram> Demolitionprogram { get; set; }

        [NotMapped]
        public List<Demolitiondocument> Demolitiondocument { get; set; }


        [NotMapped]
        public List<string> ItemsDetails { get; set; }    //add from fixing program table

        [NotMapped]
        public List<decimal> DemolitionProgramId { get; set; }  //add from fixing program table


        [NotMapped]
        public List<string> ChecklistDetails { get; set; }    //add from fixing checklist table

        [NotMapped]
        public List<decimal> DemolitionChecklistId { get; set; }  //add from fixing checklist table

        [NotMapped]
        public List<IFormFile> DocumentDetails { get; set; }    //add from fixing document table

        [NotMapped]
        public List<decimal> DemolitionDocumentId { get; set; }  //add from fixing document table


        //[NotMapped]
        //public List<IFormFile> Document { get; set; }

        public EncroachmentRegisteration Encroachment { get; set; }
        //public virtual EncroachmentRegisteration Encroachment { get; set; }
        public ICollection<Fixingchecklist> Fixingchecklist { get; set; }
        public ICollection<Fixingprogram> Fixingprogram { get; set; }
        public ICollection<Fixingdocument> Fixingdocument { get; set; }
        public ICollection<Demolitionpoliceassistenceletter> Demolitionpoliceassistenceletter { get; set; }
        public ICollection<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }
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
