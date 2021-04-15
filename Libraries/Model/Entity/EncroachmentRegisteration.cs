using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class EncroachmentRegisteration:AuditableEntity<int>
    {

        public EncroachmentRegisteration()
        {
            Fixingdemolition = new HashSet<Fixingdemolition>();

            DetailsOfEncroachment = new HashSet<DetailsOfEncroachment>();
            EncroachmentFirFileDetails = new HashSet<EncroachmentFirFileDetails>();
            EncroachmentLocationMapFileDetails = new HashSet<EncroachmentLocationMapFileDetails>();
            EncroachmentPhotoFileDetails = new HashSet<EncroachmentPhotoFileDetails>();
        }
        public int? WatchWardId { get; set; }

        [Required(ErrorMessage = "Department is Mandatory Field", AllowEmptyStrings = false)]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Zone is Mandatory Field", AllowEmptyStrings = false)]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = "Division is Mandatory Field", AllowEmptyStrings = false)]
        public int DivisionId { get; set; }
       
        [Required(ErrorMessage = "Locality is Mandatory Field", AllowEmptyStrings = false)]
        public int LocalityId { get; set; }

        [Required(ErrorMessage = "Encroachment Date is Mandatory Field")]
        public DateTime EncrochmentDate { get; set; }

        [Required(ErrorMessage = "Khasra No is Mandatory Field")]
        public string KhasraNo { get; set; }

        [Required(ErrorMessage = "Area is Mandatory Field")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal Area { get; set; }

        public string EncroacherName { get; set; }

        [Required(ErrorMessage = "Status of Land is Mandatory Field")]
        public string StatusOfLand { get; set; }

        [Required(ErrorMessage = "Possession is Mandatory Field")]
        public string IsPossession { get; set; }

        [Required(ErrorMessage = "Possession Type is Mandatory Field")]
        public string PossessionType { get; set; }
        public int? OtherDepartment { get; set; }

        [Required(ErrorMessage = "Police Station is Mandatory Field")]
        public string PoliceStation { get; set; }

        [Required(ErrorMessage = "Security Guard on Duty is Mandatory Field")]
        public string SecurityGuardOnDuty { get; set; }

        [Required(ErrorMessage = "Remarks is Mandatory Field")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Location Address with Landmark is Mandatory Field")]
        public string LocationAddressWithLandMark { get; set; }
        public byte IsActive { get; set; }
        public int? ApprovedStatus { get; set; }
        public int? PendingAt { get; set; }
        public virtual Department Department { get; set; }
        public virtual Department OtherDepartmentNavigation { get; set; }
        public virtual Division Division { get; set; }
        public virtual Locality Locality { get; set; }
        public Watchandward WatchWard { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<DetailsOfEncroachment> DetailsOfEncroachment { get; set; }
        public virtual ICollection<EncroachmentFirFileDetails> EncroachmentFirFileDetails { get; set; }
        public virtual ICollection<EncroachmentLocationMapFileDetails> EncroachmentLocationMapFileDetails { get; set; }
        public virtual ICollection<EncroachmentPhotoFileDetails> EncroachmentPhotoFileDetails { get; set; }

        [NotMapped]
        public List<IFormFile> PhotoFile { get; set; }
        [NotMapped]
        public List<IFormFile> LocationMapFile { get; set; }
        [NotMapped]
        public List<IFormFile> Firfile { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        [NotMapped]
        public List<string> NameOfStructure { get; set; }
        [NotMapped]
        public List<string> ConstructionStatus { get; set; }
        [NotMapped]
        public List<decimal> AreaApprox { get; set; }
        [NotMapped]
        public List<string> Type { get; set; }
        [NotMapped]
        public List<int> DateOfEncroachment { get; set; }

        [NotMapped]
        public List<string> ReligiousStructure { get; set; }

        [NotMapped]
        public List<decimal> CountOfStructure { get; set; }
        [NotMapped]
        public List<string> ReferenceNoOnLocation { get; set; }
        public ICollection<Fixingdemolition> Fixingdemolition { get; set; }

        [NotMapped]
        public string ApprovalStatus { get; set; }

        [NotMapped]
        public string ApprovalRemarks { get; set; }
        [NotMapped]
        public DateTime? Date { get; set; }
       
    }
}
