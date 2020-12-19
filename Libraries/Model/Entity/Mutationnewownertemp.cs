using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Mutationnewownertemp : AuditableEntity<int>
    {
        public int MutationId { get; set; }
        public string NewOwnerName { get; set; }
        public string NewOwnerFather { get; set; }
        public string NewOwnerGender { get; set; }
        public string NewOwnerAddress { get; set; }
        public string NewOwnerMobile { get; set; }
        public string NewOwnerEmail { get; set; }
        public string NewOwnerAadhar { get; set; }
        public string NewOwnerPan { get; set; }
        public string PresentOwnerPhoto { get; set; }
        public string PresentOwnerSign { get; set; }
        
        public byte? IsActive { get; set; }

        public Mutationdetails Mutation { get; set; }
    }
}
