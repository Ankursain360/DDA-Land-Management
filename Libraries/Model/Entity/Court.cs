using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Court : AuditableEntity<int>
    {


        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public byte IsActive { get; set; }

    }
}
