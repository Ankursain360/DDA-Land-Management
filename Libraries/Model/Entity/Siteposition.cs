using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Siteposition : AuditableEntity<int>
    {
        public Siteposition()
        {
            Jointsurveysitepositionmapped = new HashSet<Jointsurveysitepositionmapped>();
        }
        public string Name { get; set; }
        public byte? IsActive { get; set; }

        public ICollection<Jointsurveysitepositionmapped> Jointsurveysitepositionmapped { get; set; }
    }
}
