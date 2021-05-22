using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class DamageRateListCreateDto : AuditableDto<int>
    {
       // [Required(ErrorMessage = "Property Type is mandatory", AllowEmptyStrings = false)]
        public int PropertyId { get; set; }

      //  [Required(ErrorMessage = "Date Range is mandatory", AllowEmptyStrings = false)]
        public int DateRangeId { get; set; }

       // [Required(ErrorMessage = "Locality is mandatory", AllowEmptyStrings = false)]
        public int LocalityId { get; set; }

        public string  StartDate { get; set; }
        public string EndDate { get; set; }

        [RegularExpression(@"((\d+)((\.\d{1,3})?))$", ErrorMessage = "Please enter valid integer or decimal number with 3 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Total Area; Max 18 digits")]
        public decimal Rate { get; set; }
    }
}
