
using Dto.Master;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public partial class Actiontakenbydda : AuditableEntity<int>
    {
      
        public int RequestForProceedingId { get; set; }
        public string HandedTakenByDda { get; set; }
        [Required(ErrorMessage = "Date Of HandedOver/TakenOver by DDA is Mandatory")]
        public DateTime HandedTakenByDdadate { get; set; }
        public string PlotRestored { get; set; }
        public string CurrentStatus { get; set; }
        public string Document { get; set; }
        public byte? IsActive { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public Requestforproceeding RequestForProceeding { get; set; }
        [NotMapped]
        public List<UserBindDropdownDto> UserNameList { get; set; }
    }
}
