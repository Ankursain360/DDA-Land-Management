using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
  public  class Request : AuditableEntity<int>
    {


     
        public string PproposalName { get; set; }
        public string PfileNo { get; set; }
        public string RequiringBody { get; set; }
        public string AreaLocality { get; set; }
        public string TaunderRequest { get; set; }
        public string UnitArea { get; set; }
        public string PurposeOfAcquistion { get; set; }
        public string LayoutPlan { get; set; }
        public string Remarks { get; set; }
        public byte IsActive { get; set; }
    



    }
}
