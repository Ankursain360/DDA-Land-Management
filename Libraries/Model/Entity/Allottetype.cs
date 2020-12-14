using Libraries.Model.Common;
using System;
using System.Collections.Generic;

namespace Libraries.Model.Entity
{
    public partial class Allottetype : AuditableEntity<int>
    {
       
        public int DamagePayeeRegisterId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? Date { get; set; }
        public string AtsgpadocumentPath { get; set; }
       
        public byte? IsActive { get; set; }

        public Damagepayeeregister DamagePayeeRegister { get; set; }
    }
}
