using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
namespace Libraries.Model.Entity
{
    public class Comsubencroacherstype : AuditableEntity<int>
    {
        public DateTime EncroachStartDate { get; set; }
        public DateTime EncroachEndDate { get; set; }
        public string EncroachName { get; set; }
        public byte IsActive { get; set; }
    }
}
