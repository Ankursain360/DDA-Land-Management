using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{ 


    public partial class Demolitionstructuredetails:AuditableEntity<int>
    {
        public Demolitionstructuredetails()
        {
            Demolitionstructure = new HashSet<Demolitionstructure>();
            Demolitionstructureafterdemolitionphotofiledetails = new HashSet<Demolitionstructureafterdemolitionphotofiledetails>();
            Demolitionstructurebeforedemolitionphotofiledetails = new HashSet<Demolitionstructurebeforedemolitionphotofiledetails>();
            Areareclaimedrpt = new HashSet<Areareclaimedrpt>();
            Demolishedstructurerpt = new HashSet<Demolishedstructurerpt>();
        }
        public int FixingDemolitionId { get; set; }
        [Required(ErrorMessage = " Department is mandatory")]
        public int? DepartmentId { get; set; }
        [Required(ErrorMessage = " Zone is mandatory")]
        public int? ZoneId { get; set; }
        [Required(ErrorMessage = " Division is mandatory")]
        public int? DivisionId { get; set; }
        [Required(ErrorMessage = " Locality is mandatory")]
        public int? LocalityId { get; set; }
        [Required(ErrorMessage = " File No is mandatory")]
        public string FileNo { get; set; }
        //public string Name { get; set; }
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = " Area is mandatory")]
        public string Area { get; set; }
        [Required(ErrorMessage = " Police Station is mandatory")]
        public string PoliceStation { get; set; }
        public string NameOfAreaSite { get; set; }
        public DateTime? EncroachmentSinceDate { get; set; }
        public DateTime? DateOfApprovalDemolition { get; set; }
        public string NameOfEncroacherIfAny { get; set; }
        public DateTime? StartOfDemolitionActionDate { get; set; }
        public DateTime? EndOfDemolitionActionDate { get; set; }
       // [Required(ErrorMessage = " Area is mandatory")]
        public decimal? AreaReclaimed { get; set; }
        public string DemilitionReportPath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public Department Department { get; set; }
        public Division Division { get; set; }
        public Locality Locality { get; set; }
        public Zone Zone { get; set; }
        public Fixingdemolition FixingDemolition { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
       
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        [NotMapped]
        public List<Structure> Structure { get; set; }
        [NotMapped]
        public List<Demolitionstructure> DemolitionStructure { get; set; }
        [NotMapped]
        public List<Int32> NoOfStructrure { get; set; }
        [NotMapped]
        public List<string> NameOfStructure { get; set; }
        [NotMapped]
        public List<int> StructrureId { get; set; }
        [NotMapped]
        public List<IFormFile> AfterPhotoFile { get; set; }
        [NotMapped]
        public List<IFormFile> BeforePhotoFile { get; set; }
        [NotMapped]
        public IFormFile DemolitionReportFile { get; set; }


        //*************  Demolishedstructurerpt***************
        [NotMapped]
        public List<DateTime?> Date1 { get; set; }
        [NotMapped]
        public List<int?> StructureId1 { get; set; }
        [NotMapped]
        public List<int?> NoOfStructureDemolished { get; set; }
        [NotMapped]
        public List<int?> NoOfStructureRemaining { get; set; }

       

        //*************  Areareclaimedrpt ***************
        [NotMapped]
        public List<DateTime?> Date2 { get; set; }
       
        [NotMapped]
        public List<decimal?> Area1 { get; set; }
        [NotMapped]
        public List<decimal?> AreaToBeReclaimed { get; set; }

        [NotMapped]
        public List<Structure> StructureList { get; set; }
        public ICollection<Demolitionstructure> Demolitionstructure { get; set; }
        public ICollection<Demolitionstructureafterdemolitionphotofiledetails> Demolitionstructureafterdemolitionphotofiledetails { get; set; }
        public ICollection<Demolitionstructurebeforedemolitionphotofiledetails> Demolitionstructurebeforedemolitionphotofiledetails { get; set; }
        public ICollection<Areareclaimedrpt> Areareclaimedrpt { get; set; }
        public ICollection<Demolishedstructurerpt> Demolishedstructurerpt { get; set; }
    }
}
