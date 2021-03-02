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

      
        public int ZoneId { get; set; }
        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public string Address { get; set; }
        public string SitePosition { get; set; }
        public decimal Bigha { get; set; }
        public decimal Biswa { get; set; }
        public decimal Biswanshi { get; set; }
        public string NatureOfStructure { get; set; }
        public DateTime JointSurveyDate { get; set; }
        public string Remarks { get; set; }
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
