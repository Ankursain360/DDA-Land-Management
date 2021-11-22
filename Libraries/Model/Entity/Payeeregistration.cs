using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Payeeregistration : AuditableEntity<int>
    {
        [Required]
        [Remote(action: "ExistName", controller: "DamagePayeeRegistration", AdditionalFields = "Id,Name")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        [Remote(action: "ExistPhoneNumber", controller: "DamagePayeeRegistration", AdditionalFields = "Id,MobileNumber")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "The EmailId field is required")]
        [Remote(action: "Existemail", controller: "DamagePayeeRegistration", AdditionalFields = "Id,EmailId")]
        public string EmailId { get; set; }
        public string IsVerified { get; set; }
        public byte? IsActive { get; set; }
        
        [NotMapped]
        [StringLength(4)]
        public string CaptchaCode { get; set; }

    }
}
