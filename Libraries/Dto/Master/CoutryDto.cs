using Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class CoutryDto : AuditableDto<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
