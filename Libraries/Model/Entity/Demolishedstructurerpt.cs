using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Demolishedstructurerpt : AuditableEntity<int>
    {
       
        public int DemolitionStructureDetailsId { get; set; }
        public DateTime? DemolitionDate { get; set; }
        public int? StructureId { get; set; }
        public int? NoOfStructureDemolished { get; set; }
        public int? NoOfStructureRemaining { get; set; }
        public byte? IsActive { get; set; }
        

        public Demolitionstructuredetails DemolitionStructureDetails { get; set; }
        public Structure Structure { get; set; }
    }
}
