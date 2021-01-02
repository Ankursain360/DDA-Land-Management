using Dto.Common;
using System;

namespace Dto.Master
{
    public class DamageCalculationDto
    {
        public string PropertyTypeId { get; set; }
        public DateTime EncroachmentDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LocalityId { get; set; }
        public string Area { get; set; }


    }
}
