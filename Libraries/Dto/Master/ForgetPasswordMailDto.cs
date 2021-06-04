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

        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }

        [NotMapped]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
    }
}
