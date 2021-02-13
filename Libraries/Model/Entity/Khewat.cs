using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Khewat : AuditableEntity<int>
    {
       
        public string Name { get; set; }
        public string Number { get; set; }
        public byte IsActive { get; set; }
        

    }
}
