using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public partial class Areaunit : AuditableEntity<int>
    {
        public Areaunit()
        {
            Doortodoorsurvey = new HashSet<Doortodoorsurvey>();
        } 
        public string Name { get; set; }
        public byte IsActive { get; set; } 
        public ICollection<Doortodoorsurvey> Doortodoorsurvey { get; set; }
    }
}
