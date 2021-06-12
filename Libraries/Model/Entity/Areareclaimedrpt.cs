using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Areareclaimedrpt : AuditableEntity<int>
    {
       
        public int DemolitionStructureDetailsId { get; set; }
        public DateTime? DemolitionDate { get; set; }
        public decimal? AreaReclaimed { get; set; }
        public decimal? AreaToBeReclaimed { get; set; }
        public byte? IsActive { get; set; }
       

        public Demolitionstructuredetails DemolitionStructureDetails { get; set; }
    }
}
