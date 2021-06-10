using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public partial class Newlandappealdetail : AuditableEntity<int>
    {
        [Required(ErrorMessage = "DemandListNo is Mandatory")]
        public string DemandListNo { get; set; }
        [Required(ErrorMessage = "EnmSno is Mandatory")]

        public string EnmSno { get; set; }
        [Required(ErrorMessage = "AppealNo is Mandatory")]
        public string AppealNo { get; set; }
        [Required(ErrorMessage = "AppealByDept name is Mandatory")]
        public string AppealByDept { get; set; }
        [Required(ErrorMessage = "Date Of Appeal is Mandatory")]
        public DateTime? DateOfAppeal { get; set; }

        public string PanelLawer { get; set; }

        public string Department { get; set; }

        [Required(ErrorMessage = "Status is mandatory")]
        public byte IsActive { get; set; }

    }
}
