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
            Gisclose = new HashSet<Gisclose>();
            Gisclosetext = new HashSet<Gisclosetext>();
            Gisdashed = new HashSet<Gisdashed>();
            Gisdim = new HashSet<Gisdim>();
            Gisdimtext = new HashSet<Gisdimtext>();
            Gisencroachment = new HashSet<GISEncroachment>();
            Gisfieldboun = new HashSet<Gisfieldboun>();
            Gisgosha = new HashSet<Gisgosha>();
            Gisgrid = new HashSet<Gisgrid>();
            Gisinner = new HashSet<Gisinner>();
            Giskachapakaline = new HashSet<Giskachapakaline>();
            Giskhasraboundary = new HashSet<Giskhasraboundary>();
            Giskhasraline = new HashSet<Giskhasraline>();
            Giskhasrano = new HashSet<Giskhasrano>();
            Giskilla = new HashSet<Giskilla>();
            Gisnala = new HashSet<GISnala>();
            Gisnali = new HashSet<Gisnali>();
            Gisrailwayline = new HashSet<Gisrailwayline>();
            Gisroad = new HashSet<Gisroad>();
            Gissaheda = new HashSet<Gissaheda>();
            Gistext = new HashSet<Gistext>();
            Gistrijunction = new HashSet<Gistrijunction>();
            Gisvillageboundary = new HashSet<Gisvillageboundary>();
            Gisvillagetext = new HashSet<Gisvillagetext>();
            Giszero = new HashSet<Giszero>();
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
        public ICollection<Gisclose> Gisclose { get; set; }
        public ICollection<Gisclosetext> Gisclosetext { get; set; }
        public ICollection<Gisdashed> Gisdashed { get; set; }
        public ICollection<Gisdim> Gisdim { get; set; }
        public ICollection<Gisdimtext> Gisdimtext { get; set; }
        public ICollection<GISEncroachment> Gisencroachment { get; set; }
        public ICollection<Gisfieldboun> Gisfieldboun { get; set; }
        public ICollection<Gisgosha> Gisgosha { get; set; }
        public ICollection<Gisgrid> Gisgrid { get; set; }
        public ICollection<Gisinner> Gisinner { get; set; }
        public ICollection<Giskachapakaline> Giskachapakaline { get; set; }
        public ICollection<Giskhasraboundary> Giskhasraboundary { get; set; }
        public ICollection<Giskhasraline> Giskhasraline { get; set; }
        public ICollection<Giskhasrano> Giskhasrano { get; set; }
        public ICollection<Giskilla> Giskilla { get; set; }
        public ICollection<GISnala> Gisnala { get; set; }
        public ICollection<Gisnali> Gisnali { get; set; }
        public ICollection<Gisrailwayline> Gisrailwayline { get; set; }
        public ICollection<Gisroad> Gisroad { get; set; }
        public ICollection<Gissaheda> Gissaheda { get; set; }
        public ICollection<Gistext> Gistext { get; set; }
        public ICollection<Gistrijunction> Gistrijunction { get; set; }
        public ICollection<Gisvillageboundary> Gisvillageboundary { get; set; }
        public ICollection<Gisvillagetext> Gisvillagetext { get; set; }
        public ICollection<Giszero> Giszero { get; set; }
    }
}
