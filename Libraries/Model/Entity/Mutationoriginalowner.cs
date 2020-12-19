using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Mutationoriginalowner: AuditableEntity<int>
    {
        public int MutationId { get; set; }
        public string OriginalOwnerName { get; set; }
        public string OriginalOwnerFather { get; set; }
        public string OriginalOwnerGender { get; set; }
        public string OriginalOwnerAddress { get; set; }
        public string OriginalOwnerMobile { get; set; }
        public string OriginalOwnerAadhar { get; set; }
        public string OriginalOwnerPan { get; set; }
        public string Status { get; set; }
        public string PresentOwnerPhoto { get; set; }
        public string PresentOwnerSign { get; set; }
        public string PresentOwnerAadharNo { get; set; }
        public string PresentOwnerPanNo { get; set; }
       
        public byte? IsActive { get; set; }

        public Mutationdetails Mutation { get; set; }
    }
}
