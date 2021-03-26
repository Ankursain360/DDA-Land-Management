using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Allotteeevidenceupload : AuditableEntity<int>
    {
        public int RequestProceedingId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPatth { get; set; }
        public byte? IsActive { get; set; }

        public Requestforproceeding RequestProceeding { get; set; }
    }
}
