using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Licencefees : AuditableEntity<int>
    {
        [Required(ErrorMessage = " Property Type is mandatory", AllowEmptyStrings = false)]
        public int PropertyTypeId { get; set; }
        [Required(ErrorMessage = " Licence Fee is mandatory")]
        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Licence Fees; Max 18 digits")]
        public decimal LicenceFees { get; set; }
        [Required(ErrorMessage = " From Date is mandatory")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = " To Date is mandatory")]
        public DateTime ToDate { get; set; }
        [Required(ErrorMessage = " Status is mandatory")]
        public byte? IsActive { get; set; }


        [NotMapped]
        public List<PropertyType> PropertyTypeList { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}
