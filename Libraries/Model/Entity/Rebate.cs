using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Rebate : AuditableEntity<int>
    {
        [Required]
        public int IsRebateOn { get; set; }

        [Required(ErrorMessage = "The From Date field is required")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "The To Date field is required")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "The Rebate Percentage field is required")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Rebate Percentage; Max 18 digits")]
        public decimal RebatePercentage { get; set; }

        public byte IsActive { get; set; }

        public string Scheme { get; set; }

        [NotMapped]
        public int IsRecordExist { get; set; }
    }
}


