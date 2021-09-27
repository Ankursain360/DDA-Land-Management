using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dto.Master
{
    public class ForgetPasswordMailDto
    {
        public int? Id { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 characters allowed ")]
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }

        [NotMapped]
        [StringLength(4)]
        [Required(ErrorMessage = "Captcha is mandatory")]
        public string CaptchaCode { get; set; }
    }
}
