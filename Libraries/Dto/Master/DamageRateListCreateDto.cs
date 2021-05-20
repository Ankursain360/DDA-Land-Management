using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class DamageRateListCreateDto : AuditableDto<int>
    {
        [Required(ErrorMessage = "Property Type is mandatory", AllowEmptyStrings = false)]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Date Range is mandatory", AllowEmptyStrings = false)]
        public int DateRangeId { get; set; }

        [Required(ErrorMessage = "Locality is mandatory", AllowEmptyStrings = false)]
        public int LocalityId { get; set; }
    }
}
