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


     
        public string ChecklistDescription { get; set; }
        public byte IsActive { get; set; }
      

    }
}
