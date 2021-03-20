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
    public class Leasesubpurpose : AuditableEntity<int>
    {
        public Leasesubpurpose()
        {
            Allotmententry = new HashSet<Allotmententry>();
        }

       
        public int PurposeUseId { get; set; }
        public string SubPurposeUse { get; set; }
        public byte? IsActive { get; set; }
       

        public Leasepurpose PurposeUse { get; set; }
        public ICollection<Allotmententry> Allotmententry { get; set; }
    }
}
