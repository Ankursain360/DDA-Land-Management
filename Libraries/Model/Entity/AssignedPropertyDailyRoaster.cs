using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class AssignedPropertyDailyRoaster : AuditableEntity<int>
    {
        public int DailyRoasterId { get; set; }
        public int PropertyRegistrationId { get; set; }
        public DailyRoaster DailyRoaster { get; set; }
        public Propertyregistration PropertyRegistration { get; set; }
    }
}
