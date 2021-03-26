using Libraries.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Libraries.Model.Entity
{
    public class Leasepurpose : AuditableEntity<int>
    {
        public Leasepurpose()
        {
            Allotmententry = new HashSet<Allotmententry>();
            Leasesubpurpose = new HashSet<Leasesubpurpose>();
        }

      
        public string PurposeUse { get; set; }

        public byte? IsActive { get; set; }
        

        public ICollection<Allotmententry> Allotmententry { get; set; }
        public ICollection<Leasesubpurpose> Leasesubpurpose { get; set; }
    }
}
