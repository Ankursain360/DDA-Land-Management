using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Allotteeservicesdocument : AuditableEntity<int>
    {
        public int ServiceTypeId { get; set; }
        public int ServiceId { get; set; }
        public int DocumentChecklistId { get; set; }
        public string DocumentFileName { get; set; }

        public Documentchecklist DocumentChecklist { get; set; }
        public Servicetype ServiceType { get; set; }
    }
}
