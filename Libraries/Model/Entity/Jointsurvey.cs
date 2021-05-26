using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Jointsurvey : AuditableEntity<int>
    {        
        [Required(ErrorMessage = "Village Name is Mandatory", AllowEmptyStrings = false)]
        public int VillageId { get; set; }
        [Required(ErrorMessage = "Khasra no is Mandatory", AllowEmptyStrings = false)]
        public int KhasraId { get; set; }

        public string SitePosition { get; set; }
        [Required(ErrorMessage = "Area in Bigha No is Mandatory")]
        public decimal AreaInBigha { get; set; }

        public string NatureOfStructure { get; set; }
        [Required(ErrorMessage = "Joint Survey Date is Mandatory")]
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

        [Required(ErrorMessage = "Area in Biswa No is Mandatory")]
        public decimal? AreaInBiswa { get; set; }
        public Khasra Khasra { get; set; }
        public Acquiredlandvillage Village { get; set; }
        [NotMapped]
        public List<Khasra> KhasraList { get; set; }
        [NotMapped]
        public List<Acquiredlandvillage> VillageList { get; set; }
    }
}
