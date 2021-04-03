using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public  class Timeextension : AuditableEntity<int>
    {
        [Required(ErrorMessage = "This field is mandatory ")]
        public decimal ExtensionFees { get; set; }
        [Required(ErrorMessage = "This field is mandatory ")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "This field is mandatory ")]
        public DateTime ToDate { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte? IsActive { get; set; }
        
    }
}
