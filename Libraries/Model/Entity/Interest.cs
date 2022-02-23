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
        [Required(ErrorMessage = "Property Field is Mandatory", AllowEmptyStrings = false)]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "From Date Field is Mandatory")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To Date Field is Mandatory")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Percentage Field is Mandatory")]
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
