using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
  public  class Demolitionprogrammaster : AuditableEntity<int>
    {

        [Required(ErrorMessage = "The Items field is required")]

        public string Items { get; set; }
        [Required(ErrorMessage = "The Items Type field is required")]

        public string ItemsType { get; set; }
        public byte IsActive { get; set; }
      

    }
}
