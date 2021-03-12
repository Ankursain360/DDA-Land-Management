using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libraries.Model.Entity
{
    public class Leaseapplicationdocuments : AuditableEntity<int>
    {
        public int LeaseApplicationId { get; set; }
        public int DocumentChecklistId { get; set; }
        public string DocumentFileName { get; set; }

        public Documentchecklist DocumentChecklist { get; set; }
        public Leaseapplication LeaseApplication { get; set; }
    }
}
