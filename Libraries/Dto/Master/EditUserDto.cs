using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dto.Master
{
    public class EditUserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
