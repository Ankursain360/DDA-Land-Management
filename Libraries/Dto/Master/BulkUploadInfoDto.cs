using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class BulkUploadInfoDto
    {
        [Required(ErrorMessage = "Department is required", AllowEmptyStrings = false)]
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Pdf File Location is required")]
        public string PdfLocationPath { get; set; }

        [Required(ErrorMessage = "File Upload is required")]
        public IFormFile FileUpload { get; set; }

        [NotMapped]
        public List<DepartmentDto> DepartmentList { get; set; }
    }
}
