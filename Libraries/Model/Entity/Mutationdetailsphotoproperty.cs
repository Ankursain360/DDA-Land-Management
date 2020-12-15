using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Mutationdetailsphotoproperty : AuditableEntity<int>
    {
        public int MutationDetailsId { get; set; }
        public string PhotoPropFilePath { get; set; }
        public byte IsActive { get; set; }

        public Mutationdetails MutationDetails { get; set; }
    }
}
