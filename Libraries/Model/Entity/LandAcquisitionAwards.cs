using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Libraries.Model.Entity
{
    public class LandAcquisitionAwards: AuditableEntity<int>  
    {
        public string Title { get; set; }
        public string Village { get; set; }
        public string Documents { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public IFormFile FileDocument { get; set; } 
    }
}
