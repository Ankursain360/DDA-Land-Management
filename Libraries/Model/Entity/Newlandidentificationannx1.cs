using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{ 
    public class Newlandidentificationannx1 : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Village No is Mandatory Field")]
        public string VillageName { get; set; }

        [Required(ErrorMessage = "Address is Mandatory Field")]
        public string Address { get; set; }

        public string TalukName { get; set; }
        [Required(ErrorMessage = "Taluk is Mandatory Field", AllowEmptyStrings = false)]

        public int MuncipalityId { get; set; }
        [Required(ErrorMessage = "District is Mandatory Field", AllowEmptyStrings = false)]

        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Village No is Mandatory Field")]

        public string Areaunit { get; set; }
        [Required(ErrorMessage = "Area Unit is Mandatory Field")]

        public decimal Area { get; set; }
        [Required(ErrorMessage = "Area No is Mandatory Field")]

        public decimal? AreaAcquiredEast { get; set; }
        public decimal? AreaAcquiredWest { get; set; }
        public decimal? AreaAcquiredNorth { get; set; }
        public decimal? AreaAcquiredSouth { get; set; }
        public string AreaAgriculturalmulticropped { get; set; }
        public string Reasons { get; set; }       
        public byte IsActive { get; set; }
        public District District { get; set; }
        public Muncipality Muncipality { get; set; }
        public ICollection<Newlandidentificationkhasradetailsannx1> Newlandidentificationkhasradetailsannx1 { get; set; }
    }
}
