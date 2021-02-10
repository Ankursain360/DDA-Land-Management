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
            Gisaabadi = new HashSet<Gisaabadi>();
            Gisburji = new HashSet<Gisburji>();
            Gisclean = new HashSet<GISClean>();
            Giscleantext = new HashSet<GisCleanText>();
            Gisdim = new HashSet<Gisdim>();
            Gisencroachment = new HashSet<GISEncroachment>();
            Gisgosha = new HashSet<Gisgosha>();
            Gisgrid = new HashSet<Gisgrid>();
            Gisnala = new HashSet<GISnala>();
            Gistext = new HashSet<Gistext>();
            Gistrijunction = new HashSet<Gistrijunction>();
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
        public ICollection<Gisaabadi> Gisaabadi { get; set; }
        public ICollection<Gisburji> Gisburji { get; set; }
        public ICollection<GISClean> Gisclean { get; set; }
        public ICollection<GisCleanText> Giscleantext { get; set; }
        public ICollection<Gisdim> Gisdim { get; set; }
        public ICollection<GISEncroachment> Gisencroachment { get; set; }
        public ICollection<Gisgosha> Gisgosha { get; set; }
        public ICollection<Gisgrid> Gisgrid { get; set; }
        public ICollection<GISnala> Gisnala { get; set; }
        public ICollection<Gistext> Gistext { get; set; }
        public ICollection<Gistrijunction> Gistrijunction { get; set; }
    }
}
