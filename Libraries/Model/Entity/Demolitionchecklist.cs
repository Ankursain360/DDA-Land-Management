using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;


namespace Libraries.Model.Entity
{
   public class Demolitionchecklist : AuditableEntity<int>
    {

        



        [Required(ErrorMessage = "The Checklist Description field is required")]

        public string ChecklistDescription { get; set; }
        [Required(ErrorMessage = "The Is Active field is required")]

        public byte IsActive { get; set; }
      

    }
}
