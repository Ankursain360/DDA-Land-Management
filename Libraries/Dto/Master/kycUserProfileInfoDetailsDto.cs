
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Master
{
    public class kycUserProfileInfoDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string BranchName { get; set; }
    }
}
