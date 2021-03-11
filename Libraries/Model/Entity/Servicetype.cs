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

        public string Name { get; set; }
        public byte IsActive { get; set; }

        public ICollection<Documentchecklist> Documentchecklist { get; set; }
    }
}
