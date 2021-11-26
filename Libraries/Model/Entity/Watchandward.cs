using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public partial class Watchandward : AuditableEntity<int>
    {
        public Watchandward()
        {
            Watchandwardphotofiledetails = new HashSet<Watchandwardphotofiledetails>();
            Watchandwardreportfiledetails = new HashSet<Watchandwardreportfiledetails>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
        }
        public string RefNo { get; set; }

        [Required(ErrorMessage = "Date is mandatory")]
        public DateTime? Date { get; set; }
        public int? LocalityId { get; set; }

        public int? KhasraId { get; set; }

        [Required(ErrorMessage = "Primary List No. is mandatory")]
        public int PrimaryListNo { get; set; }

        [Required(ErrorMessage = "Landmark is mandatory")]
        public string Landmark { get; set; }

        public int Encroachment { get; set; }

        [Required(ErrorMessage = "Status On Ground is mandatory")]
        public string StatusOnGround { get; set; }

        public string PhotoPath { get; set; }
        public string ReportFiletPath { get; set; }

        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public int? ApprovedStatus { get; set; }
        public string PendingAt { get; set; }
        public int? ApprovalZoneId { get; set; }


        [NotMapped]
        public List<Propertyregistration> PrimaryListNoList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        public virtual Locality Locality { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        public virtual Khasra Khasra { get; set; }

        [NotMapped]
        public List<IFormFile> Photo { get; set; }
        [NotMapped]
        public List<IFormFile> ReportFile { get; set; }
        public virtual ICollection<Watchandwardphotofiledetails> Watchandwardphotofiledetails { get; set; }
        public virtual ICollection<Watchandwardreportfiledetails> Watchandwardreportfiledetails { get; set; }

        [NotMapped]
        public int EncroachmentStatus { get; set; }

        [NotMapped]
        public string Latitude { get; set; }

        [NotMapped]
        public string Longitude { get; set; }

        public Propertyregistration PrimaryListNoNavigation { get; set; }

        [NotMapped]
        public string PlannedUnplannedLand { get; set; }

        [NotMapped]
        public string Colony { get; set; }

        [NotMapped]
        public string Sector { get; set; }

        [NotMapped]
        public string Block { get; set; }

        [NotMapped]
        public string Pocket { get; set; }

        #region Approval Related Fields
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

        #endregion
        public ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public Approvalstatus ApprovedStatusNavigation { get; set; }

    }
}
