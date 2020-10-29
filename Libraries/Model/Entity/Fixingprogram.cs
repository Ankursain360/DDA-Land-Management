using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public partial class Fixingprogram : AuditableEntity<int>
    {

        public int FixingdemolitionId { get; set; }
        public int DemolitionProgramId { get; set; }
        public string ItemsDetails { get; set; }
        public byte IsActive { get; set; }
     
        public Demolitionprogram DemolitionProgram { get; set; }
        public Fixingdemolition Fixingdemolition { get; set; }




    }
}
