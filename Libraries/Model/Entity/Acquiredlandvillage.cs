using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Acquiredlandvillage : AuditableEntity<int>
    {
        public Acquiredlandvillage()
        {
            Awardplotdetails = new HashSet<Awardplotdetails>();
            Enchroachment = new HashSet<Enchroachment>();
            Enhancecompensation = new HashSet<Enhancecompensation>();
            Jointsurvey = new HashSet<Jointsurvey>();
            Sakanidetail = new HashSet<Sakanidetail>();
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
            Undersection4plot = new HashSet<Undersection4plot>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public int TehsilId { get; set; }
        public int DistrictId { get; set; }
        public string YearofConsolidation { get; set; }
        public string TotalNoOfSheet { get; set; }
        public string Zone { get; set; }
        public string Acquired { get; set; }
        public string Circle { get; set; }
        public string WorkingVillage { get; set; }
        public int VillageTypeId { get; set; }
        public byte IsActive { get; set; }


        [NotMapped]
        public List<District> DistrictList { get; set; }
        public virtual District District { get; set; }


        [NotMapped]
        public List<Tehsil> TehsilList { get; set; }
        public virtual Tehsil Tehsil { get; set; }



        [NotMapped]
        public List<Villagetype> VillagetypeList { get; set; }
        public virtual Villagetype VillageType { get; set; }


        public virtual ICollection<Undersection4plot> Undersection4plot { get; set; }

        public virtual ICollection<Awardplotdetails> Awardplotdetails { get; set; }
        public virtual ICollection<Enchroachment> Enchroachment { get; set; }
        public virtual ICollection<Enhancecompensation> Enhancecompensation { get; set; }
        public virtual ICollection<Jointsurvey> Jointsurvey { get; set; }
        public virtual ICollection<Khasra> Khasra { get; set; }
        public virtual ICollection<Sakanidetail> Sakanidetail { get; set; }

       
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }
       



    }
}
