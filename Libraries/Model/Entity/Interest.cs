using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Interest : AuditableEntity<int>
    {
        [Required(ErrorMessage = "The Property Type field is required", AllowEmptyStrings = false)]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "The From Date field is required")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "The To Date field is required")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "The Percentage field is required")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Percentage; Max 18 digits")]
        public decimal Percentage { get; set; }

        public byte IsActive { get; set; }

        [NotMapped]
        public List<PropertyType> PropertyTypeList { get; set; }

        [NotMapped]
        public string PropertyTypeName { get; set; }

        public PropertyType Property { get; set; }

        [NotMapped]
        public int IsRecordExist { get; set; }
    }
}
