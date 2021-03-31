using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
   public class Honble : AuditableEntity<int>
    {
        public Honble()
        {
            Cancellationentry = new HashSet<Cancellationentry>();
            Requestforproceeding = new HashSet<Requestforproceeding>();
        }

        public string HonbleName { get; set; }
        public byte? IsActive { get; set; }
        public ICollection<Cancellationentry> Cancellationentry { get; set; }

        public ICollection<Requestforproceeding> Requestforproceeding { get; set; }


    }
}
