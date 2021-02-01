using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Libraries.Model.Entity
{
    public class Datastoragepartfilenodetails : AuditableEntity<int>
    {
        public int? DataStorageDetailsId { get; set; }
        public string Category { get; set; }
        public string Header { get; set; }
        public string SequenceNo { get; set; }
        public int Year { get; set; }
        public int SchemeDptBranch { get; set; }
        public int LocalityId { get; set; }
        public string Subject { get; set; }
      

        public Locality Locality { get; set; }
        public Scheme SchemeDptBranchNavigation { get; set; }
    }
}
