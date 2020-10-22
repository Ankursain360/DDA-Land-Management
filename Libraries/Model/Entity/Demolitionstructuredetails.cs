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
        }

        
        public int? DepartmentId { get; set; }
        public int? ZoneId { get; set; }
        public int? DivisionId { get; set; }
        public int? LocalityId { get; set; }
        public string FileNo { get; set; }
        public DateTime? Date { get; set; }
        public string Area { get; set; }
        public string PoliceStation { get; set; }
        public string NameOfAreaSite { get; set; }
        public DateTime? EncroachmentSinceDate { get; set; }
        public DateTime? DateOfApprovalDemolition { get; set; }
        public string NameOfEncroacherIfAny { get; set; }
        public DateTime? StartOfDemolitionActionDate { get; set; }
        public DateTime? EndOfDemolitionActionDate { get; set; }
        public decimal? AreaReclaimed { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Department Department { get; set; }
        public Division Division { get; set; }
        public Locality Locality { get; set; }
        public Zone Zone { get; set; }
        [NotMapped]
        public List<Locality> LocalityList { get; set; }
       
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }

        public ICollection<Demolitionstructure> Demolitionstructure { get; set; }
        public ICollection<Demolitionstructureafterdemolitionphotofiledetails> Demolitionstructureafterdemolitionphotofiledetails { get; set; }
        public ICollection<Demolitionstructurebeforedemolitionphotofiledetails> Demolitionstructurebeforedemolitionphotofiledetails { get; set; }
    }
}
