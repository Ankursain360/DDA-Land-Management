using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
       public class Evidancedoc : AuditableEntity<int>
    {
        public Evidancedoc()
        {
            Hearingdetails = new HashSet<Hearingdetails>();
        }

      
        public int EvidanceId { get; set; }
        public int ReqProcedingId { get; set; }
        public string DocName { get; set; }
        public string DocPath { get; set; }
        public byte? IsActive { get; set; }
       public Requestforproceeding ReqProceding { get; set; }
        public ICollection<Hearingdetails> Hearingdetails { get; set; }
    }
}
