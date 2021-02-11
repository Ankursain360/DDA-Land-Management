using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class GisCleanText : AuditableEntity<int>
    {
        public int VillageId { get; set; }
        public string Xcoordinate { get; set; }
        public string Ycoordinate { get; set; }
        public string Polygon { get; set; }
        public string Label { get; set; }
        public byte? IsActive { get; set; }
        public Village Village { get; set; }
    }
}
