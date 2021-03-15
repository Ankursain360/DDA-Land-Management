using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Servicetype : AuditableEntity<int>
    {
        public Servicetype()
        {
            Documentchecklist = new HashSet<Documentchecklist>();
        }
        [Required(ErrorMessage = "Name is mandatory ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Status is mandatory ")]
        public byte IsActive { get; set; }

        public ICollection<Documentchecklist> Documentchecklist { get; set; }
    }
}
