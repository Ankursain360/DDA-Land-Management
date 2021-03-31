
using Dto.Master;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Actiontakenbydda : AuditableEntity<int>
    {
      
        public int RequestForProceedingId { get; set; }
        public string HandedTakenByDda { get; set; }
        public DateTime HandedTakenByDdadate { get; set; }
        public string PlotRestored { get; set; }
        public string CurrentStatus { get; set; }
        public string Document { get; set; }
        public byte? IsActive { get; set; }
       

        public Requestforproceeding RequestForProceeding { get; set; }
    }
}
