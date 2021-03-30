using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Hearingdetailsphotofiledetails : AuditableEntity<int>
    {

        public int HearingDetailsId { get; set; }
        public string PhotoFilePath { get; set; }
        public byte IsActive { get; set; }

        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public virtual Hearingdetails Hearingdetails { get; set; }

        public string LattLongUrl { get; set; }
    }
}
