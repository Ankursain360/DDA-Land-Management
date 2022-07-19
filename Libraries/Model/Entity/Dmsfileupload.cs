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

        //[Remote(action: "Exist", controller: "DMSFileUpload", AdditionalFields = "Id")]
        public string FileNo { get; set; }
        public string AlloteeName { get; set; }

        [Required(ErrorMessage = " Department is Mandatory", AllowEmptyStrings =false)]
        public int DepartmentId { get; set; }
        public int? KhasraNoId { get; set; }
        public int? LocalityId { get; set; }
        public string PropertyNoAddress { get; set; }
        public string Title { get; set; }
        public string AlmirahNo { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte IsActive { get; set; }

        [Required(ErrorMessage = "Document Category Is Mandatory")]
        public int? CategoryId { get; set; }
        public Documentcategory Category { get; set; }
        public Department Department { get; set; }
        public Propertyregistration KhasraNo { get; set; }
        public Locality Locality { get; set; }

        [NotMapped]
        public List<Documentcategory> CategoriesList { get; set; }
        [NotMapped]
        public List<Department> DepartmentList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
        [NotMapped]
        public List<Village> VillageList { get; set; }

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
        public int? ZoneId { get; set; }
        public int? VillageId { get; set; }

        public Village Village { get; set; }
        public Zone Zone { get; set; }
    }
}
