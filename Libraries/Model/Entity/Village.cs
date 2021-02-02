using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Village : AuditableEntity<int>
    {
        public Village()
        {
            Nazul = new HashSet<Nazul>();
            Plot = new HashSet<Plot>();
        }

        [Required(ErrorMessage = " Zone is mandatory")]
        public int ZoneId { get; set; }
        [Required(ErrorMessage = " Village name is mandatory")]
        [Remote(action: "Exist", controller: "Village", AdditionalFields = "Id")]
        public string Name { get; set; }
        public decimal? Xcoordinate { get; set; }
        public decimal? Ycoordinate { get; set; }
        public decimal? TotalArea { get; set; }
        public string Polygon { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [Required(ErrorMessage = "Division is mandatory")]
        [NotMapped]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = " Department is mandatory")]
        [NotMapped]
        public int DepartmentId { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<Nazul> Nazul { get; set; }
        public ICollection<Plot> Plot { get; set; }

    }
}
