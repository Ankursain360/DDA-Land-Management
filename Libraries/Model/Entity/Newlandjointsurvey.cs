using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Libraries.Model.Entity
{
   public class Newlandjointsurvey : AuditableEntity<int>
    {


        public Newlandjointsurvey()
        {
            Newjointsurveyattendancedetail = new HashSet<Newjointsurveyattendancedetail>();
            Newjointsurveyreportdetail = new HashSet<Newjointsurveyreportdetail>();
        }

        [Required(ErrorMessage = "Zone is Mandatory", AllowEmptyStrings = false)]
        public int ZoneId { get; set; }
        [Required(ErrorMessage = "Village  is Mandatory", AllowEmptyStrings = false)]
        public int VillageId { get; set; }
        [Required(ErrorMessage = "Khasra No is Mandatory", AllowEmptyStrings = false)]
        public int KhasraId { get; set; }
        [Required(ErrorMessage = "Address is Mandatory", AllowEmptyStrings = false)]
        public string Address { get; set; }
        public string SitePosition { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Bigha is mandatory")]
        public decimal Bigha { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Biswa is mandatory")]
        public decimal Biswa { get; set; }
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Bigha; Max 18 digits")]
        [Required(ErrorMessage = "Biswanshi is mandatory")]
        public decimal Biswanshi { get; set; }
        public string NatureOfStructure { get; set; }
        [Required(ErrorMessage = " Joint Survey Date is mandatory")]
        public DateTime JointSurveyDate { get; set; }
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
       

        public Newlandkhasra Khasra { get; set; }
        public Newlandvillage Village { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Newjointsurveyattendancedetail> Newjointsurveyattendancedetail { get; set; }
        public ICollection<Newjointsurveyreportdetail> Newjointsurveyreportdetail { get; set; }
        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }
          //****** Attendance details *****

        [NotMapped]
        public List<string> AName { get; set; }

        [NotMapped]
        public List<string> ADesignation { get; set; }

        [NotMapped]
        public List<string> AAttendance { get; set; }
        [NotMapped]

        public List<string> DocumentName { get; set; }
        [NotMapped]

        public List<string> UploadFilePath { get; set; }
        [NotMapped]
        public List<IFormFile> Document { get; set; }


    }
}
