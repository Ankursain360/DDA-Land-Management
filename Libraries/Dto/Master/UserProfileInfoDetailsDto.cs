using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class UserProfileInfoDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int ZoneId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string ZoneName { get; set; }
    }
}
