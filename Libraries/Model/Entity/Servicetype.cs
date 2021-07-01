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
            Allotteeservicesdocument = new HashSet<Allotteeservicesdocument>();
            Documentchecklist = new HashSet<Documentchecklist>();
            Mortgage = new HashSet<Mortgage>();
            Extensionservice = new HashSet<Extension>();
        }
        [Required(ErrorMessage = "Name is mandatory ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "URL is mandatory ")]

        public string Url { get; set; }
        public int? Timeline { get; set; }
        public byte IsActive { get; set; }

        public ICollection<Allotteeservicesdocument> Allotteeservicesdocument { get; set; }
        public ICollection<Documentchecklist> Documentchecklist { get; set; }
        public ICollection<Mortgage> Mortgage { get; set; }
        public ICollection<Extension> Extensionservice { get; set; }
    }
}
