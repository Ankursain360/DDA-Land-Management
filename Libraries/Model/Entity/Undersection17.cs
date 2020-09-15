using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Undersection17 : AuditableEntity<int>
    {
       
        public int? UnderSection6Id { get; set; }
        public string Us17number { get; set; }
        public DateTime? Us17date { get; set; }
     
        public byte IsActive { get; set; }
    }
}
