using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Damagepayeepersonelinfo : AuditableEntity<int>
    {
      
        public int DamagePayeeRegisterTempId { get; set; }
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
       
        public byte? IsActive { get; set; }

        public Damagepayeeregister DamagePayeeRegister { get; set; }
    }
}
