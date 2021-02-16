using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;


namespace Libraries.Model.Entity
{
    public class Sakniowner : AuditableEntity<int>
    {
        
        public int SakniDetailId { get; set; }
        public string OwnerName { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public byte? IsActive { get; set; }
       

        public Saknidetails SakniDetail { get; set; }
    }
}
