using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Rate : AuditableEntity<int>
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public decimal RatePercentage { get; set; }

        public byte IsActive { get; set; }

        [NotMapped]
        public List<PropertyType> PropertyTypeList { get; set; }

        [NotMapped]
        public string PropertyTypeName { get; set; }

        public string Scheme { get; set; }
        public PropertyType Property { get; set; }

        [NotMapped]
        public int IsRecordExist { get; set; }
    }
}
