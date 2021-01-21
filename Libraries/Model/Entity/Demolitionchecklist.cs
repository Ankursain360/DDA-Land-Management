using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;


namespace Libraries.Model.Entity
{
   public partial class Demolitionchecklist : AuditableEntity<int>
    {

        [Required(ErrorMessage = "Checklist Description is mandatory")]

        public string ChecklistDescription { get; set; }
        [Required(ErrorMessage = "Status is mandatory")]

        public byte IsActive { get; set; }

        //[NotMapped]
        //public List<Demolitiondocument> Demolitiondocument { get; set; }



        // public virtual ICollection<Demolitionprogram> Demolitionprogrammasterdetails { get; set; }
        // public virtual ICollection<Demolitiondocument> Demolitiondocumentdetails { get; set; }
        //public ICollection<Fixingdemolition> Fixingdemolition { get; set; }

        public ICollection<Fixingchecklist> Fixingchecklist { get; set; }


    }
}
