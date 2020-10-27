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

        public Demolitionchecklist()
        {
            Demolitionprogrammasterdetails = new HashSet<Demolitionprogram>();
            Demolitiondocumentdetails = new HashSet<Demolitiondocument>();
        }



        [Required(ErrorMessage = "The Checklist Description field is required")]

        public string ChecklistDescription { get; set; }
        [Required(ErrorMessage = "The Is Active field is required")]

        public byte IsActive { get; set; }

        //[NotMapped]
        //public List<Demolitiondocument> Demolitiondocument { get; set; }



        public virtual ICollection<Demolitionprogram> Demolitionprogrammasterdetails { get; set; }
        public virtual ICollection<Demolitiondocument> Demolitiondocumentdetails { get; set; }



    }
}
