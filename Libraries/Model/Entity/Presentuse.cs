using Libraries.Model.Common;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public class Presentuse : AuditableEntity<int>
    {


        public string Name { get; set; }
        public byte IsActive { get; set; }

        public ICollection<Doortodoorsurvey> Doortodoorsurvey { get; set; }

    }
}
