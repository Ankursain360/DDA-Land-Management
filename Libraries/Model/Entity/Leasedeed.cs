using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Leasedeed : AuditableEntity<int>
    {
        [Required(ErrorMessage = "This field is Mandatory")]
        public int AllotmentId { get; set; }
        [Required(ErrorMessage = "This field is Mandatory")]
        public DateTime LeaseDeedDate { get; set; }
        public string DocumentPath { get; set; }
        public string Remarks { get; set; }
        public byte? IsActive { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        public Allotmententry Allotment { get; set; }
        [NotMapped]
        public List<Allotmententry> ApplicationList { get; set; }
    }
}
