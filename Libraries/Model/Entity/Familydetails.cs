using Libraries.Model.Common;

namespace Libraries.Model.Entity
{
    public class Familydetails : AuditableEntity<int>
    {

        public int D2dId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string FGender { get; set; }
        public byte IsActive { get; set; }


        public Doortodoorsurvey D2d { get; set; }
    }
}
