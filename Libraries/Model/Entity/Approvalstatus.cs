using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Libraries.Model.Entity
{
    public class Approvalstatus : AuditableEntity<int>
    {
        [Required(ErrorMessage = "Approval Status is mandatory")]
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}
