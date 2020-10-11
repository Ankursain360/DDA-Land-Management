using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
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
            DetailsOfEncroachments = new HashSet<DetailsOfEncroachment>();
        }
        [Required]
        public int DepartmentId { get; set; }
        [Required] 
        public int ZoneId { get; set; }
        [Required] 
        public int DivisionId { get; set; }
        [Required] 
        public int LocalityId { get; set; }
        [Required] 
        public DateTime EncrochmentDate { get; set; }
        [Required] 
        public string KhasraNo { get; set; }
        [Required] 
        public decimal Area { get; set; }
        [Required] 
        public string StatusOfLand { get; set; }
        [Required] 
        public string IsPossession { get; set; }
        [Required] 
        public string PossessionType { get; set; }
        public int? OtherDepartment { get; set; }
        public string FirfilePath { get; set; }
        [Required] 
        public string PoliceStation { get; set; }
        [Required] 
        public string SecurityGuardOnDuty { get; set; }
        public string LocationMapFilePath { get; set; }
        public string PhotoFilePath { get; set; }
        [Required]
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual Zone Zone { get; set; }
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
        [Required]
        public List<string> NameOfStructure { get; set; }
        [NotMapped]
        [Required]
        public List<decimal> AreaApprox { get; set; }
        [NotMapped]
        [Required]
        public List<string> Type { get; set; }
        [NotMapped]
        [Required]
        public List<DateTime> DateOfEncroachment { get; set; }
        [NotMapped]
        [Required]
        public List<decimal> CountOfStructure { get; set; }
        [NotMapped]
        [Required]
        public List<string> ReferenceNoOnLocation { get; set; }
        [NotMapped]
        [Required]
        public List<string> DetailsofEncroachmentcol { get; set; }
        public virtual ICollection<DetailsOfEncroachment> DetailsOfEncroachments { get; set; }
    }
}
