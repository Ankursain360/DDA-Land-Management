using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class GISclean : AuditableEntity<int>
    {
        public int Id { get; set; }
        public int VillageId { get; set; }
        public decimal? Xcoordinate { get; set; }
        public decimal? Ycoordinate { get; set; }
        public string Polygon { get; set; }
        public byte? IsActive { get; set; }
        public Village Village { get; set; }
    }
}
