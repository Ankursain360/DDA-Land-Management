using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Gisgosha : AuditableEntity<int>
    {
        public int VillageId { get; set; }
        public decimal? Xcoordinate { get; set; }
        public decimal? Ycoordinate { get; set; }
        public string Polygon { get; set; }
        public byte? IsActive { get; set; }
        public Village Village { get; set; }
    }
}
