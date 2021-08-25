// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class LoginInputModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "* User Name must not be greater than 50 character in length.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "* Password must not be greater than 50 character in length.")]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public string Data { get; set; }
        [Required]
        public string CaptchaCode { get; set; }
    }
}