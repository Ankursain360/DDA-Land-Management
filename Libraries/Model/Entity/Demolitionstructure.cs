using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Libraries.Model.Entity
{
    public partial class Demolitionstructure : AuditableEntity<int>
    {

        public int DemolitionStructureDetailsId { get; set; }
        public int StructureId { get; set; }
        public int NoOfStructrure { get; set; }
        public byte IsActive { get; set; }
        [NotMapped]
        public List<string> NameOfStructure { get; set; }
        public virtual Structure Structure { get; set; }

        public virtual Demolitionstructuredetails DemolitionStructureDetails { get; set; }
        

    }
}
