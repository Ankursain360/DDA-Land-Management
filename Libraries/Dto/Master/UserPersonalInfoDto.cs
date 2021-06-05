using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class UserPersonalInfoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Remote(action: "ExistLoginName", controller: "UserManagement", AdditionalFields = "Id")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Remote(action: "ExistEmail", controller: "UserManagement", AdditionalFields = "Id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Remote(action: "ExistPhoneNumber", controller: "UserManagement", AdditionalFields = "Id")]
        public string PhoneNumber { get; set; }
    }
}
