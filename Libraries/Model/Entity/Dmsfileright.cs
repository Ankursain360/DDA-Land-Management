using Libraries.Model.Common;
using Model.Entity;

namespace Libraries.Model.Entity
{
   public class Dmsfileright : AuditableEntity<int>
    {
      
        public int UserId { get; set; }
        public byte? Viewright { get; set; }
        public byte? Downloadright { get; set; }
    
     
        public Userprofile User { get; set; }

    }
}
