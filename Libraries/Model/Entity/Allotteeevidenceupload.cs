using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Allotteeevidenceupload : AuditableEntity<int>
    {
        public int RequestProceedingId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPatth { get; set; }
        public byte? IsActive { get; set; }

        public Requestforproceeding RequestProceeding { get; set; }

        [NotMapped]
        public IFormFile Document { get; set; }

        [NotMapped]
        public int GenerateUpload { get; set; }

        [NotMapped]
        public List<Allotteeevidenceupload> AllotteeEvidenceUploadList { get; set; }
    }
}
