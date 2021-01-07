using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public class Comratelisttypeb : AuditableEntity<int>
    {
        public int? EncroachId { get; set; }
        public int? ColonyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int SubEncroachId { get; set; }
        public decimal? Rate { get; set; }
        public byte IsActive { get; set; }
    }
}
