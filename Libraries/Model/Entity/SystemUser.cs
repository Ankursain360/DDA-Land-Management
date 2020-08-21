using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class SystemUser : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}