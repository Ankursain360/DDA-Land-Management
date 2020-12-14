using Libraries.Model.Common;
using Libraries.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public class Mutationdetailsphotoproperty : AuditableEntity<int>
    {
        public int Id { get; set; }
        public int MutationDetailsId { get; set; }
        public string PhotoPropFilePath { get; set; }
        public byte IsActive { get; set; }
       
        public Mutationdetails MutationDetails { get; set; }
    }
}
