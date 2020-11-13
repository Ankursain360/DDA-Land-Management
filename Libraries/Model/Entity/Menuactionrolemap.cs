using Libraries.Model.Common;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public class Menuactionrolemap : AuditableEntity<int>
    {
        public int MenuId { get; set; }
        public int ActionId { get; set; }
        public int RoleId { get; set; }

        public virtual Actions Action { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}