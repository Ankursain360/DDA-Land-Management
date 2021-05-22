using System;
using System.Collections.Generic;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class DamageRateListDataDto 
    {
        public int Id { get; set; }
        public int EncroachmentTypeId { get; set; }
        public int LocalityId { get; set; }
        public int SubEncroachmentId { get; set; }
        public int PropertypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Rate { get; set; }
    }
}
