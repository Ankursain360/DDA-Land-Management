using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public partial class Damagepayeepersonelinfo : AuditableEntity<int>
    {
      
        public int DamagePayeeRegisterTempId { get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AadharNo { get; set; }
        public string AadharNoFilePath { get; set; }
        public string PanNo { get; set; }
        public string PanNoFilePath { get; set; }
        public string PhotographPath { get; set; }
        public string SignaturePath { get; set; }
        public string OtherDocPath { get; set; }
        public byte? IsActive { get; set; }

        public Damagepayeeregister DamagePayeeRegister { get; set; }
    }
}
