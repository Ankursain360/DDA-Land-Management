using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Appealdetail : AuditableEntity<int>
    {
        [Required(ErrorMessage = "DemandListNo is Mandatory")]
        public string DemandListNo { get; set; }
        [Required(ErrorMessage = "EnmSno is Mandatory")]

        public string EnmSno { get; set; }
        [Required(ErrorMessage = "AppealNo is Mandatory")]
        public string AppealNo { get; set; }
        [Required(ErrorMessage = "AppealByDept name is Mandatory")]
        public string AppealByDept { get; set; }

        public string Department { get; set; }
        public int? DemandListId { get; set; }
        public DateTime? DateOfAppeal { get; set; }
        
        public string PanelLawer { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }
        public Demandlistdetails DemandList { get; set; }


    }
}
