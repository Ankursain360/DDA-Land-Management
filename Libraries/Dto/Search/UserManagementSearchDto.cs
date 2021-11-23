using Dto.Common;

namespace Dto.Search
{
    public class UserManagementSearchDto : BaseSearchDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        public string ZoneName { get; set; }
    }
}