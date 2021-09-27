using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dto.Master
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        [NotMapped]
        [StringLength(4)]
        [Required(ErrorMessage = "Captcha is mandatory")]
        public string CaptchaCode { get; set; }
    }
}
