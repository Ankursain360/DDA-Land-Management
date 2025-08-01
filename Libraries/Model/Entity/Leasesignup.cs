﻿using Libraries.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
   public class Leasesignup : AuditableEntity<int>
    {

        [StringLength(30, ErrorMessage = "Maximum 30 characters allowed ")]      
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public int? KycId { get; set; }
        public string KycStatus { get; set; }
 
        public byte? IsActive { get; set; }

        public Kycform Kyc { get; set; }

        [NotMapped]
        [StringLength(4)]
        public string CaptchaCode { get; set; }



    }
}
