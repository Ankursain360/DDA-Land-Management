using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Leasepaymenttype : AuditableEntity<int>
    {
        public Leasepaymenttype()
        {
            Leasepaymentdetails = new HashSet<Leasepaymentdetails>();
        }

       
        public string Name { get; set; }
        public byte? IsActive { get; set; }
        

        public ICollection<Leasepaymentdetails> Leasepaymentdetails { get; set; }
    }
}
