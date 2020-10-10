using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class EncroachmentRegisteration:AuditableEntity<int>
    {
        public int DepartmentId { get; set; }
        public int ZoneId { get; set; }
        public int DivisionId { get; set; }
        public int LocalityId { get; set; }
        public DateTime EncrochmentDate { get; set; }
        public string KhasraNo { get; set; }
        public decimal Area { get; set; }
        public string StatusOfLand { get; set; }
        public string IsPossession { get; set; }
        public string PossessionType { get; set; }
        public int? OtherDepartment { get; set; }
        public string FirfilePath { get; set; }
        public string PoliceStation { get; set; }
        public string SecurityGuardOnDuty { get; set; }
        public string LocationMapFilePath { get; set; }
        public string PhotoFilePath { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual Zone Zone { get; set; }
        [NotMapped]
        public IFormFile PhotoFile { get; set; }
        [NotMapped]
        public IFormFile LocationMapFile { get; set; }
        [NotMapped]
        public IFormFile Firfile { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
    }
}
