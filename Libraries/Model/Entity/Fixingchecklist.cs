using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public partial class Fixingchecklist : AuditableEntity<int>
    {

     
        public int FixingdemolitionId { get; set; }
        public int DemolitionChecklistId { get; set; }
        [NotMapped]
        public string ChecklistDetails { get; set; }

        public byte IsActive { get; set; }
     
        public virtual Demolitionchecklist DemolitionChecklist { get; set; }
        public virtual Fixingdemolition Fixingdemolition { get; set; }



    }
}
