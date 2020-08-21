using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Country : AuditableEntity<int>
    {
        public string Name { get; set; }
    }
}