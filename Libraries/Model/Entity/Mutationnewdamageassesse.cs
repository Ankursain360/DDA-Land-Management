using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Mutationnewdamageassesse : AuditableEntity<int>
    {
        public int Id { get; set; }
        public int MutationDetailsId { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public int? Gender { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public string PhotoFilePath { get; set; }
        public string SignatureFilePath { get; set; }
        public Mutationdetails MutationDetails { get; set; }
    }
}
