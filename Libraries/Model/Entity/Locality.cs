using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Locality : AuditableEntity<int>
    {
        public Locality()
        {

            Damagecalculation = new HashSet<Damagecalculation>();
            Booktransferland = new HashSet<Booktransferland>();
            Proposalplotdetails = new HashSet<Proposalplotdetails>();
            Watchandward = new HashSet<Watchandward>();
            EncroachmentRegisteration = new HashSet<EncroachmentRegisteration>();
            Propertyregistration = new HashSet<Propertyregistration>();
            MonthlyRoaster = new HashSet<MonthlyRoaster>();
            Damagepayeeregister = new HashSet<Damagepayeeregister>();
            Mutationdetails = new HashSet<Mutationdetails>();
            Demandletters = new HashSet<Demandletters>();
            Legalmanagementsystem = new HashSet<Legalmanagementsystem>();
            Datastoragedetails = new HashSet<Datastoragedetails>();
            Dmsfileupload = new HashSet<Dmsfileupload>();
        }


        [Required(ErrorMessage = " Department is mandatory", AllowEmptyStrings =false)]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = " Zone is mandatory", AllowEmptyStrings = false)]
        public int ZoneId { get; set; }

        [Required(ErrorMessage = " Division is mandatory", AllowEmptyStrings = false)]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = " Locality Name is mandatory")]
        [Remote(action: "ExistName", controller: "Locality", AdditionalFields = "Id,DepartmentId,DivisionId,ZoneId")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Locality Code is mandatory")]
        [Remote(action: "ExistCode", controller: "Locality", AdditionalFields = "Id")]
        public string LocalityCode { get; set; }
        [Required (ErrorMessage = " Landmark is mandatory")]
        public string Landmark { get; set; }
        [Required(ErrorMessage = " Locality address is mandatory")]
        public string Address { get; set; }
        [Required]
        public byte IsActive { get; set; }
        public virtual Department Department { get; set; }
        public virtual Division Division { get; set; }
        public virtual Zone Zone { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Division> DivisionList { get; set; }
        public ICollection<Propertyregistration> Propertyregistration { get; set; }
        public virtual ICollection<EncroachmentRegisteration> EncroachmentRegisteration { get; set; }
        public ICollection<Khasra> Khasra { get; set; }
        public ICollection<Booktransferland> Booktransferland { get; set; }
        public ICollection<Proposalplotdetails> Proposalplotdetails { get; set; }
        public ICollection<Watchandward> Watchandward { get; set; }
        public ICollection<Demolitionstructuredetails> Demolitionstructuredetails { get; set; }
        public ICollection<MonthlyRoaster> MonthlyRoaster { get; set; }
        public ICollection<Damagepayeeregister> Damagepayeeregister { get; set; }
        public ICollection<Mutationdetails> Mutationdetails { get; set; }
        public ICollection<Dmsfileupload> Dmsfileupload { get; set; }

        public ICollection<Legalmanagementsystem> Legalmanagementsystem { get; set; }
        public ICollection<Damagecalculation> Damagecalculation { get; set; }
        public ICollection<Demandletters> Demandletters { get; set; }
        public ICollection<Datastoragedetails> Datastoragedetails { get; set; }
    }
}
