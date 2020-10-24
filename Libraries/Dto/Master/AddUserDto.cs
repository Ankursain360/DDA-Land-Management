using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Master
{
    public class AddUserDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password between 8 to 20", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [StringLength(20, ErrorMessage = "Password between 8 to 20", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password confirm password not match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? DepartmentId { get; set; }
        public List<DepartmentDto> DepartmentList { get; set; }
        public int? ZoneId { get; set; }
        public List<ZoneDto> ZoneList { get; set; }
        public int? DistrictId { get; set; }
        public List<DistrictDto> DistrictList { get; set; }
        public int RoleId { get; set; }
        public List<RoleDto> RoleList { get; set; }
    }
}
