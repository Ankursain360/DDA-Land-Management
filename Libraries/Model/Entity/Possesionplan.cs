using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public partial class Possesionplan : AuditableEntity<int>
    {
       
        public int AllotmentId { get; set; }
        public string NorthEast { get; set; }
        public string SouthEast { get; set; }
        public string NorthWest { get; set; }
        public string SouthWest { get; set; }
        public string SitePlanFilePath { get; set; }
        public decimal? AllotedArea { get; set; }
        public decimal PossessionArea { get; set; }
        public decimal? DiffernceArea { get; set; }
        public string PossessionTakenName { get; set; }
        public DateTime? PossessionTakenDate { get; set; }
        public string PossesionHandOverName { get; set; }
        public DateTime? PossesionHandOverDate { get; set; }
        public string Remark { get; set; }
        public byte IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Allotmententry Allotment { get; set; }
    }
}
