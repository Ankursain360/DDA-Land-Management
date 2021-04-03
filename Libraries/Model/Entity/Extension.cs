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
    public class Extension : AuditableEntity<int>
    {
        public int AllotmentId { get; set; }
        public int ServiceTypeId { get; set; }
        public int LeaseApplicationId { get; set; }
        public int ExtensionPeriod { get; set; }
        public decimal? ExtentionFees { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? UserId { get; set; }
        public byte? IsActive { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }

        public Allotmententry Allotment { get; set; }
        public Leaseapplication LeaseApplication { get; set; }
        public Servicetype ServiceType { get; set; }


        [NotMapped]
        public List<Documentchecklist> Documentchecklist { get; set; }

        [NotMapped]
        public List<int> IsMandatory { get; set; }

        [NotMapped]
        public List<int> DocumentChecklistId { get; set; }

        [NotMapped]
        public List<int> ServiceId { get; set; }

        [NotMapped]
        public List<IFormFile> FileUploaded { get; set; }
        [NotMapped]
        public List<string> FileUploadedPath { get; set; }

        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }

        [NotMapped]
        public IFormFile ApprovalDocument { get; set; }

        [NotMapped]
        public List<Approvalstatus> ApprovalStatusList { get; set; }

    }
}
