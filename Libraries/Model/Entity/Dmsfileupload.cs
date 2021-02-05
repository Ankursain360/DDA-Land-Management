using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Dmsfileupload : AuditableEntity<int>
    {
        public string IsFileBulkUpload { get; set; }

        [Remote(action: "Exist", controller: "DMSFileUpload", AdditionalFields = "Id")]
        public string FileNo { get; set; }
        public string AlloteeName { get; set; }

        [Required(ErrorMessage = "Mandatory", AllowEmptyStrings =false)]
        public int DepartmentId { get; set; }
        public int? KhasraNoId { get; set; }
        public int? LocalityId { get; set; }
        public string PropertyNoAddress { get; set; }
        public string Title { get; set; }
        public string AlmirahNo { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte IsActive { get; set; }

        public Department Department { get; set; }
        public Propertyregistration KhasraNo { get; set; }
        public Locality Locality { get; set; }

        [NotMapped]
        public List<Department> DepartmentList { get; set; }

        [NotMapped]
        public List<Locality> LocalityList { get; set; }
        [NotMapped]
        public List<Propertyregistration> KhasraNoList { get; set; }

        [NotMapped]
        public IFormFile FileUpload { get; set; }

        [NotMapped]
        public IFormFile BulkUpload { get; set; }

        [NotMapped]
        public string PdfLocationPath { get; set; }
    }
}
