using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Caseyear : AuditableEntity<int>
    {

        public string Name { get; set; }
        public byte IsActive { get; set; }

    }
}
