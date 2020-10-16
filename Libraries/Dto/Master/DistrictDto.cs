using Dto.Common;

namespace Dto.Master
{
    public class DistrictDto : AuditableDto<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public byte IsActive { get; set; }
    }
}
