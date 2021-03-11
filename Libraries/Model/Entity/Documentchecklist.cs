using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Model.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libraries.Model.Entity
{
    public class Documentchecklist : AuditableEntity<int>
    {
        public int ServiceTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsMandatory { get; set; }
        public byte IsActive { get; set; }

        public Servicetype ServiceType { get; set; }
    }
}
