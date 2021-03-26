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
            Premiumrate = new HashSet<Premiumrate>();
            Groundrent = new HashSet<Groundrent>();
            Licencefees = new HashSet<Licencefees>();
            Premiumrate = new HashSet<Premiumrate>();
            Documentcharges = new HashSet<Documentcharges>();
        }

       
        public int PurposeUseId { get; set; }
        public string SubPurposeUse { get; set; }
        public byte? IsActive { get; set; }


        public Leasepurpose PurposeUse { get; set; }
        public ICollection<Allotmententry> Allotmententry { get; set; }
        public ICollection<Premiumrate> Premiumrate { get; set; }
        public ICollection<Groundrent> Groundrent { get; set; }
        public ICollection<Licencefees> Licencefees { get; set; }
        public ICollection<Documentcharges> Documentcharges { get; set; }
    }
}
