using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
   public class Demolitiondocument : AuditableEntity<int>
    {

        [Required(ErrorMessage = "The Document Name field is required")]

        public string DocumentName { get; set; }
        [Required(ErrorMessage = "The Is Mandatory field is required")]

        public string IsMandatory { get; set; }
        public byte IsActive { get; set; }
       // public virtual ICollection<Fixingdemolition> Fixingdemolition { get; set; }
        public virtual ICollection<Fixingdocument> Fixingdocument { get; set; }



    }
}
