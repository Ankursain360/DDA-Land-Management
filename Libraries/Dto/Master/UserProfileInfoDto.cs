using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dto.Master
{
    public class UserProfileInfoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int? DepartmentId { get; set; }

        [NotMapped]
        public List<DepartmentDto> DepartmentList { get; set; }
        public int? ZoneId { get; set; }
        [NotMapped]
        public List<ZoneDto> ZoneList { get; set; }
        public int? DistrictId { get; set; }
        [NotMapped]
        public List<DistrictDto> DistrictList { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public int? RoleId { get; set; }

        [NotMapped]
        public List<RoleDto> RoleList { get; set; }
    }
}
