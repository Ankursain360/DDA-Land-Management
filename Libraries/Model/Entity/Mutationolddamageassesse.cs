using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Mutationolddamageassesse : AuditableEntity<int>
    {
        
        public int MutationDetailsId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DateGpadead { get; set; }
        public string GpastafilePath { get; set; }
        public byte IsActive { get; set; }

        public Mutationdetails MutationDetails { get; set; }

    }
}
