using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
   public class Newlandjointsurvey : AuditableEntity<int>
    {


        public int VillageId { get; set; }
        public int KhasraId { get; set; }
        public string SitePosition { get; set; }
        public decimal AreaInBigha { get; set; }
        public string NatureOfStructure { get; set; }
        public DateTime? JointSurveyDate { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
   
        public int ZoneId { get; set; }
        public string Address { get; set; }
        public string SurveyReport { get; set; }
        public string AnyOtherDocument { get; set; }
        public int? Attendance { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public decimal? AreaInBiswa { get; set; }

        public Newlandkhasra Khasra { get; set; }
        public Newlandvillage Village { get; set; }
        public Zone Zone { get; set; }

        [NotMapped]
        public List<Newlandkhasra> KhasraList { get; set; }
        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }



    }
}
