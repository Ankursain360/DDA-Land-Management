using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Demolitionpoliceassistenceletter : AuditableEntity<int>
    {
        public int FixingDemolitionId { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public string FilePath { get; set; }
        public Fixingdemolition FixingDemolition { get; set; }
    }
}
