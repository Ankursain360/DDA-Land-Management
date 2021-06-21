using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class Surveyuserrole : AuditableEntity<int>
    {
        public Surveyuserrole()
        {
            Surveyuserdetail = new HashSet<Surveyuserdetail>();
        }

       
        public string Name { get; set; }
        public byte? IsActive { get; set; }
       
        public ICollection<Surveyuserdetail> Surveyuserdetail { get; set; }
    }
}
