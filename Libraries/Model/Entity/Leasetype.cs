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
    public class Leasetype : AuditableEntity<int>
    {
        public Leasetype()
        {
            Allotmententry = new HashSet<Allotmententry>();
            Kycform = new HashSet<Kycform>();
        }

       
        public string Type { get; set; }
        public byte? IsActive { get; set; }

        public ICollection<Kycform> Kycform { get; set; }
        public ICollection<Allotmententry> Allotmententry { get; set; }
    }
}
