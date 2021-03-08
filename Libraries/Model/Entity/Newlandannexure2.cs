using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Newlandannexure2 : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Requiring Body Type is mandatory")]
        public string ReqBodyType { get; set; }
        [Required(ErrorMessage = "Official Designation of Requiring Body is mandatory")]
        public string OfficialDesigOfReqBody { get; set; }
        [Required(ErrorMessage = "Purpose of Acquition is mandatory")]
        public string PurposeOfAcqDetails { get; set; }
        public string Sn1val { get; set; }
        public string Sn1Remark { get; set; }
        public string Sn2val { get; set; }
        public string Sn2Remark { get; set; }
        public string Sn3val { get; set; }
        public string Sn3Remark { get; set; }
        public string Sn4val { get; set; }
        public string Sn5val { get; set; }
        public string Sn5Remark { get; set; }
        public string Sn6val { get; set; }
        public string Sn7val { get; set; }
        public string Sn7Remark { get; set; }
        public DateTime? Sn7Date { get; set; }
        public string Sn7File { get; set; }
        public DateTime? Sn8date { get; set; }
        public string Sn8remarks { get; set; }
        public string Sn8filePath { get; set; }
        public DateTime? Sn9date { get; set; }
        public string Sn9filePath { get; set; }
        public string Sn10val { get; set; }
        public string Sn11val { get; set; }
        public string Sn12filePath { get; set; }
        [Required(ErrorMessage = "Project Year is mandatory")]
        public string ProjectYear { get; set; }
        [Required(ErrorMessage = "Project Month is mandatory")]
        public string ProjectMonth { get; set; }
        [Required(ErrorMessage = "Project cost is mandatory")]
        public decimal? ProjectCost { get; set; }
        public string OtherRemarks { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte? IsActive { get; set; }
        public int ReqId { get; set; }
            
       
       
        [NotMapped]
        public IFormFile Sn7Filep { get; set; }
        [NotMapped]
        public IFormFile Sn8File { get; set; }
        [NotMapped]
        public IFormFile Sn9File { get; set; }
        [NotMapped]
        public IFormFile Sn12File { get; set; }
    }
}
