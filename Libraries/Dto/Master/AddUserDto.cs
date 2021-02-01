using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dto.Master
{
    public class AddUserDto
    {
        public int? Id { get; set; } 

        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User Name is mandatory")]
        [Remote(action: "ExistLoginName", controller: "UserManagement", AdditionalFields = "Id")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        [StringLength(255, ErrorMessage = "Password between 8 to 20", MinimumLength = 8)]
        public string Password { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Confirm password is mandatory")]
        [StringLength(20, ErrorMessage = "Password between 8 to 20", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password confirm password not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is mandatory")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Department is mandatory", AllowEmptyStrings = false)]
        public int? DepartmentId { get; set; }

        [NotMapped]
        public List<DepartmentDto> DepartmentList { get; set; }
        public int? ZoneId { get; set; }
        [NotMapped]
        public List<ZoneDto> ZoneList { get; set; }
        public int? DistrictId { get; set; }
        [NotMapped]
        public List<DistrictDto> DistrictList { get; set; }

        [Required(ErrorMessage = "Role is mandatory", AllowEmptyStrings = false)]
        public int RoleId { get; set; }

        [NotMapped]
        public List<RoleDto> RoleList { get; set; }
    }
}
