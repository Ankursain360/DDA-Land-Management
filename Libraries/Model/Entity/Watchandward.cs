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
        [Required (ErrorMessage ="Please fill date")]
        public DateTime? Date { get; set; }
        public int? LocalityId { get; set; }

        public int? KhasraId { get; set; }

        [Required(ErrorMessage = "Please select Primary List No.")]
        public int PrimaryListNo { get; set; }

        [Required(ErrorMessage = "Please fill Landmark")]
        public string Landmark { get; set; }

        public int Encroachment { get; set; }

        [Required(ErrorMessage = "Status On Ground is required")]
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

        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }

        public ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public Approvalstatus ApprovedStatusNavigation { get; set; }

    }
}
