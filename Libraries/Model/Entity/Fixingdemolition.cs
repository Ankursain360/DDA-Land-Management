using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;

namespace Libraries.Model.Entity
{
    public partial class Fixingdemolition : AuditableEntity<int>
    {

        public Fixingdemolition()
        {
            Fixingchecklist = new HashSet<Fixingchecklist>();
            Fixingprogram = new HashSet<Fixingprogram>();
            Fixingdocument = new HashSet<Fixingdocument>();
        }

        public int EncroachmentId { get; set; }
        public byte IsActive { get; set; }


        [NotMapped]
        public List<Demolitionchecklist> Demolitionchecklist { get; set; }
        [NotMapped]
        public List<Demolitionprogram> Demolitionprogram { get; set; }

        [NotMapped]
        public List<Demolitiondocument> Demolitiondocument { get; set; }


        [NotMapped]
        public List<string> ItemsDetails { get; set; }    //add from fixing program table

        [NotMapped]
        public List<decimal> DemolitionProgramId { get; set; }  //add from fixing program table


        [NotMapped]
        public List<string> ChecklistDetails { get; set; }    //add from fixing checklist table

        [NotMapped]
        public List<decimal> DemolitionChecklistId { get; set; }  //add from fixing checklist table




        public EncroachmentRegisteration Encroachment { get; set; }
        //public virtual EncroachmentRegisteration Encroachment { get; set; }
       public  ICollection<Fixingchecklist> Fixingchecklist { get; set; }
        public ICollection<Fixingprogram> Fixingprogram { get; set; }
        public ICollection<Fixingdocument> Fixingdocument { get; set; }




    }
}
