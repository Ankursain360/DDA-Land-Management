using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
  public  class Demolitionprogram : AuditableEntity<int>
    {

        [Required(ErrorMessage = " Items is mandatory")]

        public string Items { get; set; }
        [Required(ErrorMessage = "Items Type is mandatory")]

        public string ItemsType { get; set; }
        [Required(ErrorMessage = "Status is mandatory", AllowEmptyStrings = false)]
        public byte IsActive { get; set; }
       // public virtual ICollection<Fixingdemolition> Fixingdemolition { get; set; }
        public virtual ICollection<Fixingprogram> Fixingprogram { get; set; }



    }
}
