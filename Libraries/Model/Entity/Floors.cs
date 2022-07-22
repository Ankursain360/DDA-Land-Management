using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public partial class Floors : AuditableEntity<int>
    {
        public Floors()
        {
            Doortodoorsurvey = new HashSet<Doortodoorsurvey>();
            Newdamageselfassessmentfloordetail = new HashSet<Newdamageselfassessmentfloordetail>();
        } 
        public string Name { get; set; }
        public byte IsActive { get; set; } 
        public ICollection<Doortodoorsurvey> Doortodoorsurvey { get; set; }
        public ICollection<Newdamageselfassessmentfloordetail> Newdamageselfassessmentfloordetail { get; set; }
    }
}
