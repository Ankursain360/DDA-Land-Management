using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Newlandvillage : AuditableEntity<int>
    {
        public Newlandvillage()
        {
            Newlandacquistionproposalplotdetails = new HashSet<Newlandacquistionproposalplotdetails>();
            Newlandkhasra = new HashSet<Newlandkhasra>();
            Newlandus4plot = new HashSet<Newlandus4plot>();
            Newlandus17plot = new HashSet<Newlandus17plot>();
            Newlandus22plot = new HashSet<Newlandus22plot>();
            Newlandus6plot = new HashSet<Newlandus6plot>();
            Newlandawardmasterdetail = new HashSet<Newlandawardmasterdetail>();

            Newlandenhancecompensation = new HashSet<Newlandenhancecompensation>();
        }
        [Required(ErrorMessage = "Village name is mandatory field")]
        public string Name { get; set; }

        public string Code { get; set; }
        [Required(ErrorMessage = "District name is mandatory", AllowEmptyStrings = false)]
        public int? DistrictId { get; set; }
        [Required(ErrorMessage = "Tehsil name is mandatory", AllowEmptyStrings = false)]
        public int? TehsilId { get; set; }
        public int? YearofConsolidation { get; set; }
        public int? TotalNoOfSheet { get; set; }
        [Required(ErrorMessage = "Zone name is mandatory", AllowEmptyStrings = false)]
        public int? ZoneId { get; set; }
        public string Acquired { get; set; }
        public string Circle { get; set; }
        public string WorkingVillage { get; set; }

        public string VillageType { get; set; }
        public byte? IsActive { get; set; }
        [NotMapped]
        public List<District> DistrictList { get; set; }
        [NotMapped]
        public List<Zone> ZoneList { get; set; }

        [NotMapped]
        public List<Tehsil> TehsilList { get; set; }

        [NotMapped]
        public List<Newlandvillage> VillageList { get; set; }

        //     public  ICollection<LandCategory> LandCategory { get; set; }
        public District District { get; set; }
        public Tehsil Tehsil { get; set; }
        public Zone Zone { get; set; }
        
        public ICollection<Newlandkhasra> Newlandkhasra { get; set; }
        public ICollection<Newlandus4plot> Newlandus4plot { get; set; }
        public ICollection<Newlandus17plot> Newlandus17plot { get; set; }
        public ICollection<Newlandus6plot> Newlandus6plot { get; set; }
        public ICollection<Newlandus22plot> Newlandus22plot { get; set; }
        public ICollection<Newlandacquistionproposalplotdetails> Newlandacquistionproposalplotdetails { get; set; }
        public ICollection<Newlandenhancecompensation> Newlandenhancecompensation { get; set; }
        public ICollection<Newlandjointsurvey> Newlandjointsurvey { get; set; }
        public ICollection<Newlandawardmasterdetail> Newlandawardmasterdetail { get; set; }
    }
}
