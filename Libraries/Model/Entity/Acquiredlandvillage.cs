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
            Khasra = new HashSet<Khasra>();
            Undersection22plotdetails = new HashSet<Undersection22plotdetails>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? YearofConsolidation { get; set; }
        public int? TotalNoOfSheet { get; set; }
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

        public District District { get; set; }
        public Tehsil Tehsil { get; set; }
        public Zone Zone { get; set; }
        public ICollection<Khasra> Khasra { get; set; }
        public ICollection<Undersection22plotdetails> Undersection22plotdetails { get; set; }



    }
}
