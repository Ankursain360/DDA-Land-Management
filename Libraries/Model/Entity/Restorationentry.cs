using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Restorationentry : AuditableEntity<int>
    {
       
        public int? AllotmentId { get; set; }
        public int? Cancellationid { get; set; }
        [Required(ErrorMessage = "Restoration Order No is mandatory ")]
        public string RestorationOrder { get; set; }
        [Required(ErrorMessage = "Restoration Date is mandatory ")]
        public DateTime? RestorationDate { get; set; }
        [Required(ErrorMessage = "Restoration Remarks is mandatory ")]
        public string RestorationRemarks { get; set; }
        public byte? IsActive { get; set; } 
        public Allotmententry Allotment { get; set; }
        public Cancellationentry Cancellation { get; set; }
    }
}
