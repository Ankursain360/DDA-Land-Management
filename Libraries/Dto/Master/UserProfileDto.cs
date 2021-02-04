using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class UserProfileDto : AuditableDto<int>
    {
        public UserProfileDto()
        {

        }

        public int? RoleId { get; set; }
        public int? ZoneId { get; set; }
        public int? DistrictId { get; set; }
        public short? IsActive { get; set; }
        public int UserId { get; set; }
        public int? DepartmentId { get; set; }
        public int? BranchId { get; set; }
        public virtual BranchDto Branch { get; set; }

        public virtual DepartmentDto Department { get; set; }
        public virtual DistrictDto District { get; set; }
        public virtual UserDto User { get; set; }
        public virtual RoleDto Role { get; set; }
        public virtual ZoneDto Zone { get; set; }
    }
}
