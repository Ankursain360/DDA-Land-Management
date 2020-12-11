using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class DamagePayeeRegistration : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string IsVerified { get; set; }
        public byte? IsActive { get; set; }


    }
}
