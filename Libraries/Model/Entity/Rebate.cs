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

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public decimal RebatePercentage { get; set; }

        public byte IsActive { get; set; }

        public string Scheme { get; set; }

        [NotMapped]
        public int IsRecordExist { get; set; }
    }
}


