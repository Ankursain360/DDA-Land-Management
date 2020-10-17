using Dto.Common;
using System;
using System.Collections.Generic;

namespace Dto.Master
{
    public class UserDto : AuditableDto<int>
    {
        public UserDto()
        {
            Userprofile = new List<UserProfileDto>();
        }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public short? IsDefaultPassword { get; set; }
        public DateTime? PasswordSetDate { get; set; }

        public List<UserProfileDto> Userprofile { get; set; }
    }
}
